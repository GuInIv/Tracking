using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tracking.Models;

namespace Tracking
{
    class CommandExecutor
    {
        private IUnitOfWork unitOfWork;
        private TrackingDataView trackingDataView;
        public IEnumerable<TrackingDataView> ExecuteResult { get; set; }
        public string CommandName { get; set; }
        public string InputData { get; set; }

        public CommandExecutor()
        {
            unitOfWork = new UnitOfWork();
            ExecuteResult = new List<TrackingDataView>();
            trackingDataView = new TrackingDataView();
        }

        public void Execute()
        {
            ParseInputString();
            if (!string.IsNullOrWhiteSpace(this.CommandName))
            {
                var commandDb = this.GetCommandInstance(CommandName);
                commandDb.Execute();
                ExecuteResult = (commandDb as IExecuteResult)?.TrackingResult;
                if (ExecuteResult != null)
                {
                    PrintTrackingResult();
                }
            }
        }

        public void PrintTrackingResult()
        {
            foreach (var result in ExecuteResult)
            {
                Console.WriteLine($"{result.FirstName} {result.LastName} {result.Age}");
                foreach (var point in result.Points)
                {
                    Console.WriteLine($"{point.X} {point.Y}");
                }
            }
        }

        public bool IsCommandAdd(string firstString)
        {
            var match = GetRegexMatch(firstString);
            if (match.Success)
            {
                var groups = match.Groups;
                if (groups["commandName"].Value.ToLower() == "add")
                    return true;
            }

            return false;
        }

        private void ParseInputString()
        {
            var match = GetRegexMatch(this.InputData.ToString());
            this.CommandName = string.Empty;

            if (match.Success)
            {
                var groups = match.Groups;
                CommandName = groups["commandName"].Value.ToLower();
                trackingDataView.CipherKey = !string.IsNullOrWhiteSpace(groups["key"].Value) ? groups["key"].Value : null;
                trackingDataView.FirstName = !string.IsNullOrWhiteSpace(groups["userFirstName"].Value) ? groups["userFirstName"].Value : null;
                trackingDataView.LastName = !string.IsNullOrWhiteSpace(groups["userLastName"].Value) ? groups["userLastName"].Value : null;
                trackingDataView.Age = !string.IsNullOrWhiteSpace(groups["age"].Value) ? byte.Parse(groups["age"].Value) : default;
                trackingDataView.PatternUserFirstName = !string.IsNullOrWhiteSpace(groups["patternUserFirstName"].Value) ? groups["patternUserFirstName"].Value.Trim() : null;
                var Points = new List<Point>();
                for (int i = 0; i < groups["points"].Captures.Count; i++)
                {
                    string[] nums = groups["points"].Captures[i].Value.Split(' ');
                    Points.Add(new Point()
                    {
                        X = Convert.ToDouble(nums[0].Trim().Replace(".", ",")),
                        Y = Convert.ToDouble(nums[1].Trim().Replace(".", ","))
                    });
                }

                trackingDataView.Points = Points.Count != 0 ? Points : null;
            }
        }

        private Match GetRegexMatch(string input)
        {
            var options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            string pattern = @"^(?<commandName>add|read|find)\W+(?<patternUserFirstName>[a-zA-Z]+\*?\W)?-k\W+(?<key>\w+)(\W+(?<userFirstName>[a-zA-Z]+)\W+(?<userLastName>[a-zA-Z]+)\W+(?<age>\d{1,3})(?<points>\W+(\d+[.,]?\d*)\s(\d+[.,]?\d*))+)?";
            return Regex.Match(input, pattern, options);
        }

        private IExecute GetCommandInstance(string name)
        {
            var isCommandClassificationExists = CommandNaming.Commands.Keys.Contains(name);

            if (!isCommandClassificationExists)
            {
                throw new Exception($@"The ""{name}"" instance not found");
            }

            var commandTypeToCreate = CommandNaming.Commands[name];
            var command = (IExecute)Activator.CreateInstance(commandTypeToCreate, unitOfWork, trackingDataView);

            return command;
        }
    }
}