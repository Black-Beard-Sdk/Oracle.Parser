using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models
{


    public enum StatusKind
    {
        Information,
        Warning,
        Error
    }

    public class Anomaly
    {

        public Anomaly(Ichangable item, string message, string propertyInFailed, StatusKind status)
        {
            this.Item = item;
            this.Message = message;
            this.PropertyName = propertyInFailed;
            this.Status = status;
        }


        /// <summary>
        /// subject of the anomaly
        /// </summary>
        public Ichangable Item { get; private set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// property in failed
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Severity of the information
        /// </summary>
        public StatusKind Status { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Item.GetOwner());
            sb.Append(".");
            sb.Append(Item.GetName());

            sb.Append(" ");
            sb.Append(this.Status.ToString());
            sb.Append(" ");

            sb.Append(" [");
            sb.Append(Message);
            sb.Append("]");

            return sb.ToString();
        }

    }

}
