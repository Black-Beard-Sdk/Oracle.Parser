﻿using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Specialized;
using Bb.Oracle.Models.TextReferentials;
using System.Collections;

namespace Bb.Oracle.Structures.Models
{
    public partial class OracleDatabase
    {

        public OracleDatabase()
        {

            this.Partitions = new PartitionCollection() { Parent = this };
            this.Tablespaces = new TablespaceCollection() { Parent = this };
            this.Packages = new PackageCollection() { Parent = this };
            this.Sequences = new SequenceCollection() { Parent = this };
            this.Types = new TypeCollection() { Parent = this };
            this.Procedures = new ProcedureCollection() { Parent = this };

            this.Tables = new TableCollection() { Parent = this };
            this.Triggers = new TriggerCollection() { Parent = this };
            this.Indexes = new IndexCollection() { Parent = this };
            this.Constraints = new ConstraintCollection() { Parent = this };

            this.Synonyms = new SynonymCollection() { Parent = this };
            this.Grants = new GrantCollection() { Parent = this };
            
            this.References = new ReferentialNames();

        }

        public void Accept(Contracts.IOracleModelVisitor visitor)
        {
            this.Partitions.Accept(visitor);
            this.Tablespaces.Accept(visitor);
            this.Packages.Accept(visitor);
            this.Sequences.Accept(visitor);
            this.Types.Accept(visitor);
            this.Procedures.Accept(visitor);
            this.Tables.Accept(visitor);
            this.Triggers.Accept(visitor);
            this.Indexes.Accept(visitor);
            this.Constraints.Accept(visitor);
            this.Synonyms.Accept(visitor);
            this.Grants.Accept(visitor);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        internal void Changes(IndexedCollection indexedCollection, NotifyCollectionChangedEventArgs arg)
        {

            if (this.CollectionChanged != null)
                this.CollectionChanged(indexedCollection, arg);

            switch (arg.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var i in arg.NewItems)
                        if (i is ItemBase item)
                            this.References.Add(item);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var i in arg.OldItems)
                        if (i is ItemBase item)
                            this.References.Remove(item);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (var i in arg.OldItems)
                        if (i is ItemBase item)
                            this.References.Remove(item);
                    foreach (var i in arg.NewItems)
                        if (i is ItemBase item)
                            this.References.Add(item);
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                default:
                    break;

            }


        }

        [JsonIgnore]
        public Oracle.Models.TextReferentials.ReferentialNames References { get; set; }


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
        public TableCollection Tables { get; set; }

        /// <summary>
        /// Triggers
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TriggerCollection" />.");
        /// </returns>
        public TriggerCollection Triggers { get; set; }

        /// <summary>
        /// Indexes
        /// </summary>
        /// <returns>		
        /// Objet <see cref="IndexCollection" />.");
        /// </returns>
        public IndexCollection Indexes { get; set; }

        /// <summary>
        /// Constraints
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ConstraintCollection" />.");
        /// </returns>
        public ConstraintCollection Constraints { get; set; }

        /// <summary>
        /// Procedures
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ProcedureCollection" />.");
        /// </returns>
        public ProcedureCollection Procedures { get; set; }

        /// <summary>
        /// Types
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TypeCollection" />.");
        /// </returns>

        public TypeCollection Types { get; set; }

        /// <summary>
        /// Synonymes
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SynonymCollection" />.");
        /// </returns>
        public SynonymCollection Synonyms { get; set; }

        /// <summary>
        /// Sequences
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SequenceCollection" />.");
        /// </returns>
        public SequenceCollection Sequences { get; set; }

        /// <summary>
        /// Grants
        /// </summary>
        /// <returns>		
        /// Objet <see cref="GrantCollection" />.");
        /// </returns>
        public GrantCollection Grants { get; set; }

        /// <summary>
        /// Packages
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PackageCollection" />.");
        /// </returns>
        public PackageCollection Packages { get; set; }

        /// <summary>
        /// Tablespaces
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TablespaceCollection" />.");
        /// </returns>
        public TablespaceCollection Tablespaces { get; set; }

        /// <summary>
        /// Partitions
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionCollection" />.");
        /// </returns>
        public PartitionCollection Partitions { get; set; }

        public void Initialize()
        {

            this.Partitions.Initialize();

            this.Tables.Initialize();
            this.Triggers.Initialize();
            this.Indexes.Initialize();
            this.Constraints.Initialize();

            this.Types.Initialize();
            this.Sequences.Initialize();
            this.Procedures.Initialize();
            this.Grants.Initialize();
            this.Synonyms.Initialize();

            this.Tablespaces.Initialize();
            this.Packages.Initialize();

            
        }

        /// <summary>
        /// Annotations for the debug. (this property is not serialized)
        /// </summary>
        [JsonIgnore]
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

