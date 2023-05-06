using System;


namespace Lab07
{
    public class ProcEventArgs : EventArgs
    {
        public int id { get; set; }
        public int n { get; set; }

        public ProcEventArgs(int id)
        {
            this.id = id;
        }
    }
}
