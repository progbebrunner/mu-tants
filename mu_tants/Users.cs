//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mu_tants
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public int user_id { get; set; }
        public string image { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Nullable<int> role { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public Nullable<int> fav_artist { get; set; }
        public Nullable<int> fav_album { get; set; }
    
        public virtual Albums Albums { get; set; }
        public virtual Artists Artists { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
