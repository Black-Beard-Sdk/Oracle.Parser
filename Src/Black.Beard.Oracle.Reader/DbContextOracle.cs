﻿using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader
{

    public class DbContextOracle : DbContextBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextOracle"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public DbContextOracle(OracleManager manager)
        {
            this.Manager = manager;
        }

        /// <summary>
        /// Gets or sets the Database structure.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public OracleDatabase Database { get; set; }

        /// <summary>
        /// Gets or sets the use.
        /// </summary>
        /// <value>
        /// The use.
        /// </value>
        public Func<string, bool> Use { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exclude resolving code].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclude code]; otherwise, <c>false</c>.
        /// </value>
        public bool ExcludeCode { get; set; }
        public Version Version { get; internal set; }
    }

}
