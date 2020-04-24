namespace Exe.CompareModel
{

    public class ArgumentContext
    {

        public ArgumentContext(params string[] args)
        {
            string[] argsResult = ArgumentHelper.MapArguments(args, this);
        }

        public string Source { get; set; }

        public string Target { get; set; }

        public string PathSource { get; set; }

        public string PathTarget { get; set; }

        public string Output { get; set; }

        public string UrlTfs { get; set; }

        public string TfsProjectName { get; set; }

        public string ConfigApplication { get; set; }
        public bool Excel { get; internal set; }
        public bool Mail { get; internal set; }
        public string RuleFilename { get; internal set; }

        public string[] ToArray()
        {
            return ArgumentHelper.ToArray(this);
        }

    }

}
