namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IndexColumnModel : ItemBase
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rule
        /// </summary>
        public string Rule { get; set; }

        /// <summary>
        /// Asc
        /// </summary>
        public bool Asc { get; set; }

        public IndexModel Parent { get; set; }

        public void Initialize()
        {

        }


    }

}
