namespace Record
{
    public delegate void Callback();

    public delegate void Callback<T>(T arg1);

    public delegate void Callback<T, U>(T arg1, U arg2);

    public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);

    public delegate void Callback<T, U, V, X>(T arg1, U arg2, V arg3, X arg4);

    public delegate bool ActionHandler();

    public delegate bool ActionHandler<T>(T arg1);

    public delegate bool ActionHandler<T, U>(T arg1, U arg2);

    public delegate bool ActionHandler<T, U, V>(T arg1, U arg2, V arg3);

    public delegate bool ActionHandler<T, U, V, X>(T arg1, U arg2, V arg3, X arg4);

    public delegate ECommandReply CommandHandler<T>(T command) where T : ICommand;

    public delegate ECommandReply CommandHandle(params object[] args);
}