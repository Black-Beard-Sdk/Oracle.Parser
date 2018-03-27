//using Bb.Oracle.Contracts;
//using Bb.Oracle.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace Bb.Oracle.Files
//{

//    public class FileManager : IFileManager
//    {

//        private string defaultFileMask = "{0}.{1}.sql";
//        private DirectoryInfo dir;
//        private FolderSolutionSection section;

//        public event EventHandler<DirectoryArg> DirectoryCreated;
//        public event EventHandler<FileArg> FileCreated;

//        public FileManager(DirectoryInfo dir, FolderSolutionSection section = null)
//        {
//            this.dir = dir;

//            if (section != null)
//                this.section = section;
//            else
//                this.section = FolderSolutionSection.Configuration;

//        }

//        public void StartNewFile(Ichangable item, IchangeVisitor visitor)
//        {
//            visitor.Clear();
//        }

//        public void Write(Ichangable item, IchangeVisitor visitor)
//        {

//            policyEnum policy = policyEnum.DeleteBefore;
//            FileInfo f = GetPath(item, out policy);

//            if (!f.Directory.Exists)
//            {
//                f.Directory.Create();
//                if (this.DirectoryCreated != null)
//                    this.DirectoryCreated(this, new DirectoryArg(f.Directory));
//            }

//            visitor.Write(f, policy);

//            if (this.FileCreated != null)
//                this.FileCreated(this, new FileArg(f));

//        }

//        private FileInfo GetPath(Ichangable item, out policyEnum policy)
//        {

//            switch (item.KindModel)
//            {

//                case KindModelEnum.Sequence:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Sequence, "Sequences", defaultFileMask, item);

//                case KindModelEnum.Index:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Index, "Indexes", defaultFileMask, item);

//                case KindModelEnum.Privilege:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Privilege, "Grants", defaultFileMask, item);

//                case KindModelEnum.Constraint:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Constraint, "Constraints", defaultFileMask, item);

//                case KindModelEnum.Package:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Package, "Packages", defaultFileMask, item);

//                case KindModelEnum.Procedure:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Procedure, "Procedures", defaultFileMask, item);

//                case KindModelEnum.Function:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Function, "Functions", defaultFileMask, item);

//                case KindModelEnum.Table:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Table, "Tables", defaultFileMask, item);

//                case KindModelEnum.Trigger:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Trigger, "Triggers", defaultFileMask, item);

//                case KindModelEnum.Type:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Type, "Types", defaultFileMask, item);

//                case KindModelEnum.View:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.View, "Views", defaultFileMask, item);

//                case KindModelEnum.MaterializedView:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.MaterializedView, "MaterializedViews", defaultFileMask, item);

//                case KindModelEnum.Tablespace:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Tablespace, "Tablespaces", defaultFileMask, item);

//                case KindModelEnum.Cluster:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Cluster, "Clusters", defaultFileMask, item);

//                case KindModelEnum.DatabaseLink:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.DatabaseLink, @"DatabaseLinks", defaultFileMask, item);

//                case KindModelEnum.Directory:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Directory, "Directories", defaultFileMask, item);

//                case KindModelEnum.IndexPartition:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.IndexPartition, @"Indexes\Partitions", defaultFileMask, item);

//                case KindModelEnum.IndexSubPartition:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.IndexSubPartition, @"Indexes\SubPartitions", defaultFileMask, item);

//                case KindModelEnum.IndexType:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.IndexType, @"Indexes\Types", defaultFileMask, item);

//                case KindModelEnum.Lob:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Lob, "Lobs", defaultFileMask, item);

//                case KindModelEnum.LobPartition:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.LobPartition, @"Lobs\Partitions", defaultFileMask, item);

//                case KindModelEnum.Operator:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Operator, "Operators", defaultFileMask, item);

//                case KindModelEnum.Queue:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Queue, "Queues", defaultFileMask, item);

//                case KindModelEnum.Rule:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Rule, "Rules", defaultFileMask, item);

//                case KindModelEnum.Schedule:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.Schedule, "Schedules", defaultFileMask, item);

//                case KindModelEnum.TablePartition:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.TablePartition, @"Tables\TablePartition", defaultFileMask, item);

//                case KindModelEnum.TablesubPartition:
//                    policy = policyEnum.DeleteBefore;
//                    return ResolveFilePath(KindModelEnum.TablesubPartition, @"Tables\TableSubPartition", defaultFileMask, item);

//                default:
//                    if (System.Diagnostics.Debugger.IsAttached)
//                        throw new NotImplementedException(item.GetType().Name);
//                    break;
//            }

//            throw new NotImplementedException();

//        }

//        private Regex reg = new Regex(@"{\d}", RegexOptions.None);

//        private FileInfo ResolveFilePath(KindModelEnum kindModelEnum, string defaultFolder, string defaultMask, Ichangable item)
//        {

//            var folder = ResolveFolder(kindModelEnum, defaultFolder);
//            var name = ResolveName(kindModelEnum, defaultMask);

//            string file = string.Empty;

//            var cnt = reg.Matches(name).Count;

//            if (cnt == 0)
//                file = name;
//            else if (cnt == 1)
//                file = string.Format(name, item.GetName());
//            else if (cnt == 2)
//                file = string.Format(name, item.GetName(), kindModelEnum.ToString());

//            var path = Path.Combine(this.dir.FullName, item.GetOwner(), folder, file);

//            var result = new FileInfo(path);

//            return result;

//        }

//        private string ResolveFolder(KindModelEnum kindModelEnum, string defaultFolder)
//        {

//            if (section != null)
//            {
//                var folder = section.Folders[kindModelEnum];
//                if (folder != null)
//                    return folder.FolderName;
//            }

//            return defaultFolder;

//        }


//        private string ResolveName(KindModelEnum kindModelEnum, string defaultmask)
//        {

//            if (section != null)
//            {
//                var folder = section.Folders[kindModelEnum];
//                if (folder != null)
//                    return folder.FilenameMask;
//            }

//            return defaultmask;

//        }






//        public void StartBlock(KindModelEnum kindModelEnum)
//        {

//        }

//        public void CloseBlock(KindModelEnum kindModelEnum)
//        {

//        }

//    }


//}
