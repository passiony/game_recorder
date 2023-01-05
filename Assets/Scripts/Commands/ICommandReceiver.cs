using System.Collections.Generic;
using System;

namespace Record
{
    public class ICommandReceiver
    {
        private Dictionary<ECommand, Delegate> mCommands = new Dictionary<ECommand, Delegate>();

        public void AddCommand<T>(ECommand cmd, CommandHandler<T> handler) where T : ICommand
        {
            if (!this.mCommands.ContainsKey(cmd))
            {
                this.mCommands.Add(cmd, handler);
            }
        }

        public ECommandReply Command<T>(T cmd) where T : ICommand
        {
            Delegate del = null;
            mCommands.TryGetValue(cmd.type, out del);
            if (del == null)
            {
                return ECommandReply.N;
            }

            return (ECommandReply)del.DynamicInvoke(cmd);

            // CommandHandler<T> callback = del as CommandHandler<T>;
            // if (callback == null)
            // {
            //     Debug.LogError(typeof(T).ToString() + cmd);
            //     return ECommandReply.N;
            // }
            // return callback(cmd);
        }
    }
}