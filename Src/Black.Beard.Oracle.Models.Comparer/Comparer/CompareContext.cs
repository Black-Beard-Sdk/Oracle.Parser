using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Models.Comparer
{

    public class CompareContext
    {

        /// <summary>
        /// If true by pass all test on partitions
        /// </summary>
        public bool IgnorePartitions { get; internal set; }

        /// <summary>
        /// If true by pass all test on tables
        /// </summary>
        public bool IgnoreTables { get; internal set; }

        /// <summary>
        /// If true by pass all test on grants
        /// </summary>
        public bool IgnoreGrants { get; internal set; }

        /// <summary>
        /// If true by pass all test on Packages
        /// </summary>
        public bool IgnorePackages { get; internal set; }

        /// <summary>
        /// If true by pass all test on Procedures
        /// </summary>
        public bool IgnoreProcedures { get; internal set; }

        /// <summary>
        /// If true by pass all test on sequences
        /// </summary>
        public bool IgnoreSequences { get; internal set; }

        /// <summary>
        /// If true by pass all test on Synonyms
        /// </summary>
        public bool IgnoreSynonyms { get; internal set; }

        /// <summary>
        /// If true by pass all test on types
        /// </summary>
        public bool IgnoreTypes { get; internal set; }

        /// <summary>
        /// if true by pass all test targets vs sources
        /// </summary>
        public bool IgnoreRevert { get; internal set; }

    }

}
