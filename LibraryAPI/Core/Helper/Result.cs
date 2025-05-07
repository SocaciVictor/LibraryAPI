namespace LibraryAPI.Core.Helper
{
    public class Result
    {
        public List<string> Errors { get; set; } = new List<string>();

        public bool HasErrors => Errors.Any();
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }
    }
}
