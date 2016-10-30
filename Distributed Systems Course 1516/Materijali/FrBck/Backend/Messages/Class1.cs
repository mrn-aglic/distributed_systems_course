using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class TransformationJob
    {
        public string Text { get; private set; }

        public TransformationJob(string text)
        {
            Text = text;
        }
    }

    public class TransformationResult
    {
        public string Text { get; private set; }

        public TransformationResult(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class JobFailed
    {
        public string Reason { get; private set; }

        public JobFailed(string reason)
        {
            Reason = reason;
        }
    }

    public class BackendRegistration { }
}
