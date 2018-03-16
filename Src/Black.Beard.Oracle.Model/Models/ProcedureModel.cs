using Bb.Oracle.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public partial class ProcedureModel : ItemBase, Ichangable
    {

        public ProcedureModel()
        {

            this.Arguments = new ArgumentCollection() { Parent = this};

            this.ResultType = new ProcedureResult() { Parent = this };

        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Package Name
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sub Program Id
        /// </summary>
        public int SubProgramId { get; set; }

        /// <summary>
        /// Is Function
        /// </summary>
        public bool IsFunction { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Arguments
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ArgumentCollection" />.");
        /// </returns>
        public ArgumentCollection Arguments { get; set; }

        /// <summary>
        /// ResultType
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ProcedureResult" />.");
        /// </returns>
        public ProcedureResult ResultType { get; set; } 



        public bool IsValid(HashSet<string> list)
        {

            if (list.Contains(PackageName) || list.Contains(Owner))
                return true;

            return false;

        }

        public bool IsStruct
        {
            get
            {
                return this.ResultType.Columns.Count != 0 && this.ResultType.Columns.Cast<ColumnModel>().Where(c => c.Name != "arg0").Any();
            }
        }

        //private string _returnType;
        //public string ReturnType
        //{
        //    get
        //    {
        //        if (_returnType == null)
        //        {
        //            if (IsRecord)
        //                _returnType = string.Empty;

        //            else
        //            {
        //                Type t1;
        //                var t2 = this.Result.Cast<ColumnModel>().Where(c => c.ColumnName == "arg0").FirstOrDefault();
        //                if (t2 != null)
        //                {
        //                    t1 = Type.GetType(t2.CsType);
        //                    if (t1 != null)
        //                    {
        //                        _returnType = t1.FullName;
        //                    }
        //                    else
        //                    {
        //                        _returnType = "void";
        //                    }
        //                }
        //                else
        //                {

        //                    t1 = Type.GetType(CsType);
        //                    if (t1 != null)
        //                    {
        //                        _returnType = t1.FullName;
        //                    }
        //                    else
        //                        _returnType = "void";
        //                }
        //            }
        //        }

        //        return _returnType;
        //    }
        //}


        //ArgumentsInfoModel _arg1;
        //public ArgumentsInfoModel GetArguments()
        //{

        //    if (_arg1 == null)
        //    {

        //        var lst = this.Arguments.Cast<ArgumentModel>().Where(c => c.Type.DataLevel == 0 && !string.IsNullOrEmpty(c.Type.DataType)).Select(c => new ArgumentInfoModel(c) { }).OrderBy(c => c.Argument.Sequence).ToList();

        //        var f = lst.Where(c => !c.Argument.In && string.IsNullOrEmpty(c.Argument.ArgumentName)).FirstOrDefault();
        //        if (f != null)
        //            f.Argument.ArgumentName = "result";

        //        var l = lst.Where(c => c.Argument.In).LastOrDefault();

        //        if (l != null)
        //            l.Comma = string.Empty;

        //        //var istruct = lst.Where(c => !c.Argument.In).Any();

        //        _arg1 = new ArgumentsInfoModel() { Arguments = lst /*, Istruct = istruct*/ };

        //    }

        //    return _arg1;
        //}


        private bool? _isComplexAccess;
        public bool IsComplexAccess
        {
            get
            {

                if (!this._isComplexAccess.HasValue)
                {
                    foreach (var item in Arguments)
                        if (item.Name.Split(':').Length > 0)
                        {
                            this._isComplexAccess = true;
                            return this._isComplexAccess.Value;
                        }

                    foreach (ColumnModel item in this.ResultType.Columns)
                    {
                        if (item.Name.Split(':').Length > 0)
                        {
                            this._isComplexAccess = true;
                            return this._isComplexAccess.Value;
                        }
                    }

                    this._isComplexAccess = false;

                }

                return this._isComplexAccess.Value;

            }
        }


        public string CustomName
        {
            get
            {

                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(this.Owner))
                {
                    sb.Append(this.Owner);
                    sb.Append(".");
                }

                if (!string.IsNullOrEmpty(this.PackageName))
                {
                    sb.Append(this.PackageName);
                    sb.Append(".");
                }

                sb.Append(this.Name);

                return sb.ToString();

            }
        }

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
            visitor.Alter(this, source as ProcedureModel, propertyName);
        }

        public override KindModelEnum KindModel
        {
            get
            {
                if (IsFunction)
                    return KindModelEnum.Function;
                return KindModelEnum.Procedure;
            }
        }

        public override void Initialize()
        {

            this.Arguments.Initialize();

        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.Owner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        public string GetCodeSource()
        {
            return Utils.Unserialize(this.Code, true);
        }


    }

}