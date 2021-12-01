using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using SmallTool.Extensions;
using SmallTool.Models;
using SmallTool.Models.BranchCommands;

namespace SmallTool.Controllers
{
    public class CommandController : Controller
    {
        private readonly BranchHandler _branchHandler;
        private Dictionary<string, IBranchCommand> _commands;
        
        public CommandController(BranchHandler branchHandler)
        {
            _branchHandler = branchHandler;
            _commands = new Dictionary<string, IBranchCommand>
            {
                {"RunCommand", new RunCommand()},
                {"DeleteCommand", new DeleteCommand()},
                {"FetchCommand", new FetchCommand()},
                {"LocalOpenFolderCommand", new OpenFolderCommand(tuple => throw new NotImplementedException())},
                {"RemoteOpenFolderCommand", new OpenFolderCommand(tuple => throw new NotImplementedException())},
            };
        } 
        
        [HttpPost]
        public IActionResult Execute([FromBody]string command, string branch)
        {
            if (!_commands.ContainsKey(command))
                return Problem("Wrong command name");
            var branchTuple = _branchHandler.BranchTuples.FirstOrDefault(tuple =>
                    tuple.GetName().Match(name => name == branch, false));
            if (branchTuple == null)
                return Problem("Wrong branch name");
            _commands[command].Execute(branchTuple);
            return Ok();
        }
    }
}