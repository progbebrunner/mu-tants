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
    
    public partial class Artists_To_Musicians
    {
        public int artist_id { get; set; }
        public Nullable<int> musician_id { get; set; }
    
        public virtual Artists Artists { get; set; }
        public virtual Musicians Musicians { get; set; }
    }
}
