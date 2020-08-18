﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ArgsProcessor
    {
        private readonly ArgsActions actions;

        public ArgsProcessor(ArgsActions actions)
        {
            this.actions = actions;
        }

        public void Process(string[] args)
        {
            foreach (var arg in args)
            {
                actions[arg]?.Invoke();
            }
        }

    }
    public class ArgsActions
    {
        readonly private Dictionary<string, Action> argsActions = new Dictionary<string, Action>();

        public Action this[string s]
        {
            get
            {
                Action action;
                Action defaultAction = () => { };
                return argsActions.TryGetValue(s, out action) ? action : defaultAction;
            }
        }

        public void SetOption(string s, Action a)
        {
            argsActions[s] = a;
        }
    }
}
