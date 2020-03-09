namespace JobHorizon.CustomModel
{
    using System;
    using System.Collections.Generic;

    public partial class Messege
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Msg { get; set; }
        public System.DateTime CreatedAt { get; set; }
    }
}