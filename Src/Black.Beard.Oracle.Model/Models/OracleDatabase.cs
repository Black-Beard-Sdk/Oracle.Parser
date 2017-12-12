using Newtonsoft.Json;
using System.IO;

namespace Bb.Oracle.Models
{
    public partial class OracleDatabase
    {

        public string Name { get; set; }

        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Available Owner
        /// </summary>
        public string AvailableOwner { get; set; }

        /// <summary>
        /// Source Script
        /// </summary>
        public bool SourceScript { get; set; }

        /// <summary>
        /// Tables
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TableCollection" />.");
        /// </returns>
        public TableCollection Tables { get; set; } = new TableCollection();

        /// <summary>
        /// Procedures
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ProcedureCollection" />.");
        /// </returns>
        public ProcedureCollection Procedures { get; set; } = new ProcedureCollection();

        /// <summary>
        /// Types
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TypeCollection" />.");
        /// </returns>

        public TypeCollection Types { get; set; } = new TypeCollection();

        /// <summary>
        /// Synonymes
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SynonymCollection" />.");
        /// </returns>
        public SynonymCollection Synonymes { get; set; } = new SynonymCollection();

        /// <summary>
        /// Sequences
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SequenceCollection" />.");
        /// </returns>
        public SequenceCollection Sequences { get; set; } = new SequenceCollection();

        /// <summary>
        /// Grants
        /// </summary>
        /// <returns>		
        /// Objet <see cref="GrantCollection" />.");
        /// </returns>
        public GrantCollection Grants { get; set; } = new GrantCollection();

        /// <summary>
        /// Packages
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PackageCollection" />.");
        /// </returns>
        public PackageCollection Packages { get; set; } = new PackageCollection();

        /// <summary>
        /// Tablespaces
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TablespaceCollection" />.");
        /// </returns>
        public TablespaceCollection Tablespaces { get; set; } = new TablespaceCollection();

        /// <summary>
        /// Partitions
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionCollection" />.");
        /// </returns>
        public PartitionCollection Partitions { get; set; } = new PartitionCollection();

        //public TriggerModel ResolveTrigger(string key)
        //{

        //    TriggerModel trigger = null;

        //    foreach (TableModel table in Tables)
        //    {

        //        trigger = table.Triggers[key];

        //        if (trigger != null)
        //            break;

        //    }

        //    return trigger;

        //}

        public bool ResolveIndex(string key, out IndexModel index)
        {

            index = null;

            foreach (TableModel table in Tables)
            {

                index = table.Indexes[key];

                if (index != null)
                    break;

            }

            return index != null;

        }

        public void Initialize()
        {

            foreach (TableModel item in Tables)
            {

                item.Parent = this;

                foreach (ConstraintModel c in item.Constraints)
                    c.Initialize();

            }

            foreach (SequenceModel item in this.Sequences)
            {
                item.Parent = this;
                item.Initialize();
            }

            foreach (ProcedureModel item in this.Procedures)
            {
                item.Parent = this;
                item.Initialize();
                
            }

            foreach (TypeItem item in this.Types)
            {
                item.Parent = this;
                item.Initialize();                
            }

            foreach (TableModel item in Tables)
                item.Initialize();

            foreach (SequenceModel item in this.Sequences)
                item.Initialize();

            foreach (ProcedureModel item in this.Procedures)
                item.Initialize();

            foreach (TypeItem item in this.Types)
                item.Initialize();

            foreach (GrantModel item in this.Grants)
                item.Parent = this;

            foreach (SynonymModel item in this.Synonymes)
                item.Parent = this;

        }

        /// <summary>
        /// Annotations for the debug. (this property is not serialized)
        /// </summary>
        public string Annotation { get; set; }


        public void WriteFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            using (FileStream stream = file.OpenWrite())
            {
                string sz = JsonConvert.SerializeObject(this, Formatting.Indented);
                byte[] ar = System.Text.Encoding.UTF8.GetBytes(sz);
                stream.Write(ar, 0, ar.Length);
            }
        }

        public static OracleDatabase ReadFlie(string filename)
        {
            FileInfo file = new FileInfo(filename);
            using (StreamReader stream = file.OpenText())
            {
                OracleDatabase result = JsonConvert.DeserializeObject<OracleDatabase>(stream.ReadToEnd());
                result.Initialize();
                return result;
            }
        }

    }


}

