using EastpointFlightReportCommon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EastpointFlightReportCommon.Parser
{
    public class FlightReportFileParser
    {
        enum InstructionLineType
        {
            AddRoute,
            AddAircraft,
            AddGeneralPassenger,
            AddAirlinePassenger,
            AddLoyaltyPassenger,
        }

        private Dictionary<string, InstructionLineType> InstructionLineTypes = new Dictionary<string, InstructionLineType>()
        {
            { "add route ", InstructionLineType.AddRoute },
            { "add aircraft ", InstructionLineType.AddAircraft },
            { "add general ", InstructionLineType.AddGeneralPassenger },
            { "add airline ", InstructionLineType.AddAirlinePassenger },
            { "add loyalty ", InstructionLineType.AddLoyaltyPassenger },
        };

        struct InstructionLine
        {
            public string Text, Instruction, Parameters;
            public InstructionLineType Type;
            public string[] ParametersArray;

            public InstructionLine(string text, string instruction, string parameters, InstructionLineType instructionLineType)
            {
                Text = text;
                Instruction = instruction;
                Parameters = parameters;
                Type = instructionLineType;
                ParametersArray = parameters.Split(' ');
            }
        }

        public FlightReport ParseFlightReport(string inputFilePath)
        {
            var flightReport = new FlightReport();

            using(var reader = new StreamReader(inputFilePath))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 0)
                        continue;

                    ParseFlightReportLine(line, flightReport);
                }
            }

            if (flightReport.Passengers.Count == 0)
                throw new Exception("Passengers are not set.");

            return flightReport;
        }

        private void ParseFlightReportLine(string line, FlightReport flightReport)
        {
            var instructionLine = GetInstructionLine(line);                            
                        
            if (flightReport.Route == null) // Route must be set first
            {
                if(instructionLine.Type != InstructionLineType.AddRoute)
                    throw new Exception("Route is not set.");
                
                flightReport.Route = new Route(instructionLine.ParametersArray);
            }            
            else if (flightReport.Aircraft == null) // Aircraft must be set first
            {
                if (instructionLine.Type != InstructionLineType.AddAircraft)
                    throw new Exception("Aircraft is not set.");

                flightReport.Aircraft = new Aircraft(instructionLine.ParametersArray);
            }
            else // Parse passengers
            {
                Passenger passenger = null;

                switch(instructionLine.Type)
                {
                    case InstructionLineType.AddRoute: // Route must be set only once
                        throw new Exception("Route is already set.");
                    case InstructionLineType.AddAircraft: // Aircraft must be set only once
                        throw new Exception("Aircraft is already set.");

                    case InstructionLineType.AddGeneralPassenger:
                        passenger = new GeneralPassenger(instructionLine.ParametersArray);
                        break;
                    case InstructionLineType.AddLoyaltyPassenger:
                        passenger = new LoyaltyPassenger(instructionLine.ParametersArray);
                        break;
                    case InstructionLineType.AddAirlinePassenger:
                        passenger = new AirlinePassenger(instructionLine.ParametersArray);
                        break;

                }

                if (passenger == null)
                    throw new Exception("Passengers are not set.");

                flightReport.Passengers.Add(passenger);
            }
        }

        private InstructionLine GetInstructionLine(string instructionLineString)
        {
            InstructionLine instructionLine;

            try
            { 
                var kvp = InstructionLineTypes.First(x => instructionLineString.StartsWith(x.Key));

                var parameters = instructionLineString.Substring(kvp.Key.Length);
                instructionLine = new InstructionLine(instructionLineString, kvp.Key, parameters, kvp.Value);
            }
            catch(Exception e)
            {
                throw new Exception($"Instruction not recognized: {instructionLineString}", e);
            }

            return instructionLine;
        }        
    }
}
