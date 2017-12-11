using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pssa.Tools.Databases.Generators.Writers
{

    public class MaterializedViewWriter
    {

        public MaterializedViewWriter(string owner, string name)
        {
            this.Owner = owner.ToUpper();
            this.Name = name.ToUpper();
        }

        public string Name { get; private set; }
        public string Owner { get; private set; }
        public string REFRESH_METHOD { get; set; }
        public string REFRESH_MODE { get; set; }
        public bool UPDATABLE { get; set; }
        public bool REWRITE { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }
        public string MASTER_ROLLBACK_SEG { get; internal set; }

        public void Write(StringBuilder sb)
        {

            //CREATE MATERIALIZED VIEW
            sb.Append("CREATE MATERIALIZED VIEW ");

            //  [ schema. ]materialized_view
            if (!string.IsNullOrEmpty(this.Owner))
            {
                sb.Append(this.Owner);
                sb.Append(".");
            }
            sb.Append(this.Name);


            //  [ '(' column_alias [, column_alias]... ] ')'
            //sb.Append(" (");
            //sb.Append(" )");

            //  [ OF [ schema. ]object_type ]
            
            scoped_table_ref_constraint(sb);

            build(sb);

            index(sb);

            // [create_mv_refresh]
            create_mv_refresh(sb);            


            //  [ FOR UPDATE ]
            if (this.UPDATABLE)
            {

            }

            //  [ { DISABLE | ENABLE }
            //    QUERY REWRITE
            //  ]
            if (this.REWRITE)
            {

            }

            //  AS subquery ;
            sb.AppendLine(string.Empty);
            sb.AppendLine("AS");
            sb.Append(this.Source.Trim().Trim(' ', '\t', '\r', '\n'));

            if (!this.Source.Trim().EndsWith(";"))
            {
                sb.AppendLine(string.Empty);
                sb.AppendLine(";");
            }
            else
            {
                sb.AppendLine(string.Empty);
            }

            //if (!string.IsNullOrEmpty(this.Comments))
            //{
            //    sb.AppendLine(string.Format(@"COMMENT ON TABLE ""{0}"".""{1}"" is '{2}';", this.Owner, this.Name, Comment.Replace("'", "''")));                
            //}

        }

        private void index(StringBuilder sb)
        {
            //  [ USING INDEX
            //    [ physical_attributes_clause
            //    | TABLESPACE tablespace
            //    ]
            //      [ physical_attributes_clause
            //      | TABLESPACE tablespace
            //      ]...
            //  | USING NO INDEX
            //  ]

        }

        private void build(StringBuilder sb)
        {

            //  { 
            //      ON PREBUILT TABLE [ { WITH | WITHOUT } REDUCED PRECISION ]
            //    | physical_properties materialized_view_props
            //  }



            // materialized_view_props ::= 
            //[column_properties]
            //[table_partitioning_clauses]
            //[CACHE | NOCACHE]
            //[parallel_clause]
            //[build_clause]

        }


        // physical_properties ::=
        //{ segment_attributes_clause
        //  [table_compression]
        //| ORGANIZATION
        //     { HEAP
        //          [segment_attributes_clause]
        //          [table_compression]
        //     | INDEX
        //          [segment_attributes_clause]
        //          index_org_table_clause
        //     | EXTERNAL
        //          external_table_clause
        //     }
        //| CLUSTER cluster(column[, column]...)
        //}

        private void scoped_table_ref_constraint(StringBuilder sb)
        {
            //  [ (scoped_table_ref_constraint) ]
        }

        private void create_mv_refresh(StringBuilder sb)
        {

            //{ REFRESH
            //  { 

            //      { FAST | COMPLETE | FORCE }
            sb.Append(" REFRESH ");
            sb.Append(this.REFRESH_METHOD);

            //      | ON { DEMAND | COMMIT }
            sb.Append(" ON ");
            sb.Append(this.REFRESH_MODE);

            //      | { START WITH | NEXT } date

            //      | WITH { PRIMARY KEY | ROWID }

            //      | USING
            //           { DEFAULT [ MASTER | LOCAL ]
            //                ROLLBACK SEGMENT
            //           | [ MASTER | LOCAL ]


            //                ROLLBACK SEGMENT rollback_segment
            if (!string.IsNullOrEmpty(MASTER_ROLLBACK_SEG))
            {
                sb.Append("ROLLBACK SEGMENT ");
                sb.Append(MASTER_ROLLBACK_SEG);
            }

            //           }
            //             [ DEFAULT [ MASTER | LOCAL ]
            //                  ROLLBACK SEGMENT
            //             | [ MASTER | LOCAL ]
            //                  ROLLBACK SEGMENT rollback_segment
            //             ]...

            //      | USING
            //           { ENFORCED | TRUSTED }
            //           CONSTRAINTS

            //  }



            //    [ { FAST | COMPLETE | FORCE }
            //    | ON { DEMAND | COMMIT }
            //    | { START WITH | NEXT } date
            //    | WITH { PRIMARY KEY | ROWID }
            //    | USING
            //         { DEFAULT [ MASTER | LOCAL ]
            //              ROLLBACK SEGMENT
            //         | [ MASTER | LOCAL ]
            //              ROLLBACK SEGMENT rollback_segment
            //         }
            //           [ DEFAULT [ MASTER | LOCAL ]
            //                ROLLBACK SEGMENT
            //           | [ MASTER | LOCAL ]
            //                ROLLBACK SEGMENT rollback_segment
            //           ]...
            //    | USING
            //         { ENFORCED | TRUSTED }
            //         CONSTRAINTS
            //    ]...
            //| NEVER REFRESH
            //}


        }
    }
}







//{ SCOPE FOR
//  ({ ref_column | ref_attribute })
//  IS[schema. ] { scope_table_name | c_alias }
//}
//  [, SCOPE FOR
//     ({ ref_column | ref_attribute })
//     IS[schema. ] { scope_table_name | c_alias }
//  ]...
