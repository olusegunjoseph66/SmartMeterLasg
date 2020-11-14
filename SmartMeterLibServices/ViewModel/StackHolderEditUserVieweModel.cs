using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMeterLibServices.ViewModel
{
    public class StackHolderEditUserVieweModel
    {

        public StackHolderEditUserVieweModel()
        {
            Roles = new List<string>();
            Claims = new List<string>();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> Claims { get; set; }

    }
}
