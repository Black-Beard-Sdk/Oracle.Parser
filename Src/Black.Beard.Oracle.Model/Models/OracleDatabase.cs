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

        public bool ResolveIndex(string key, out IndexModel index)
        {

            index = null;

            foreach (TableModel table in Tables)
                if (table.Indexes.TryGet(key, out index))
                    return true;

            return false;

        }

        public bool ResolveTrigger(string key, out TriggerModel trigger)
        {

            trigger = null;

            foreach (TableModel table in Tables)
                if (table.Triggers.TryGet(key, out trigger))
                    return true;

            return false;

        }

        public void Initialize()
        {

            foreach (TableModel item in Tables)
            {
                item.Parent = this;
                item.Initialize();
            }
            
            foreach (TypeItem item in this.Types)
            {
                item.Parent = this;
                item.Initialize();
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

            foreach (GrantModel item in this.Grants)
            {
                item.Parent = this;
            }

            foreach (SynonymModel item in this.Synonymes)
            {
                item.Parent = this;                
            }

        }

        /// <summary>
        /// Annotations for the debug. (this property is not serialized)
        /// </summary>
        public string Annotation { get; set; }


        public void WriteFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            using (StreamWriter stream = file.CreateText())
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                serializer.Serialize(stream, this);
            }
        }

        public static OracleDatabase ReadFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            using (StreamReader stream = file.OpenText())
            {
                JsonSerializer serializer = new JsonSerializer();
                OracleDatabase db = (OracleDatabase)serializer.Deserialize(stream, typeof(OracleDatabase));
                db.Initialize();
                return db;
            }
        }

    }


}

