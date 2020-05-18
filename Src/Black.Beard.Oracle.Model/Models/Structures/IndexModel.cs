using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Index
    /// </summary>
    public partial class IndexModel : ItemBase, Ichangable
    {

        public IndexModel()
        {
            this.Columns = new IndexColumnCollection() { Parent = this };
            this.BlocPartition = new BlocPartitionModel() { Parent = this };
            TableReference = new ReferenceTable() { GetDb = () => this.Root };
            this.PhysicalAttributes = new PhysicalAttributesModel() { Parent = this };
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitIndex(this);
            this.Columns.Accept(visitor);
            this.BlocPartition.Accept(visitor);
            this.PhysicalAttributes.Accept(visitor);
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Compress
        /// </summary>
        public string Compress { get; set; }

        public PhysicalAttributesModel PhysicalAttributes { get; private set; }

        /// <summary>
        /// Segment Name
        /// </summary>
        public string SegmentName { get; set; }

        /// <summary>
        /// Secure File
        /// </summary>
        public bool SecureFile { get; set; }

        /// <summary>
        /// In_ Row
        /// </summary>
        public bool In_Row { get; set; }

        /// <summary>
        /// Chunk
        /// </summary>
        public int Chunk { get; set; }

        /// <summary>
        /// Free Pools
        /// </summary>
        public string FreePools { get; set; }

        /// <summary>
        /// Cache
        /// </summary>
        public bool Cache { get; set; }
     
        /// <summary>
        /// Deduplication
        /// </summary>
        public string Deduplication { get; set; }

        /// <summary>
        /// Index Name
        /// </summary>
        [DefaultValue("")]
        public string Name { get; set; }

        /// <summary>
        /// Index Owner
        /// </summary>
        [DefaultValue("")]
        public string IndexOwner { get; set; }

        /// <summary>
        /// Index Type
        /// </summary>
        [DefaultValue("")]
        public string IndexType { get; set; }

        /// <summary>
        /// Unique
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        /// Bitmap
        /// </summary>
        public bool Bitmap { get; set; }

        /// <summary>
        /// Compression_ Prefix
        /// </summary>
        [DefaultValue("")]
        public string Compression_Prefix { get; set; }

        /// <summary>
        /// Generated
        /// </summary>
        [DefaultValue("")]
        public string Generated { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="IndexColumnCollection" />.");
        /// </returns>
        public IndexColumnCollection Columns { get; set; }

        public ReferenceTable TableReference { get; set; }

        public TableModel GetTable()
        {
            return this.TableReference.Resolve();
        }

        /// <summary>
        /// BlocPartition
        /// </summary>
        /// <returns>		
        /// Objet <see cref="BlocPartitionModel" />.");
        /// </returns>
        public BlocPartitionModel BlocPartition { get; set; }

        public void Create(IchangeVisitor visitor)
        {
            visitor.Create(this);
        }

        public void Drop(IchangeVisitor visitor)
        {
            visitor.Drop(this);
        }

        public void Alter(IchangeVisitor visitor, Ichangable source, string propertyName)
        {
            visitor.Drop(this);
            visitor.Create(this);
        }

        public override void Initialize()
        {
            TableReference.GetDb = () => this.Root;
            this.Columns.Initialize();
            this.PhysicalAttributes.Initialize();
        }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Index; }
        }

        public bool ONLINE { get; set; }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.IndexOwner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }


    }



}
