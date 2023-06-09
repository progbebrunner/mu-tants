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
    using System.Linq;
    using System.IO;

    public partial class Albums
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Albums()
        {
            this.Users = new HashSet<Users>();
        }

        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\Albums\");
        public string just_path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");

        public int album_id { get; set; }
        public string album_name { get; set; }
        public int artist_id { get; set; }
        public int length { get; set; }
        public string new_length
        {
            get 
            {
                return length.ToString();
            }
        }

        public string artist_name
        {
            get
            {
                var artists = App.Context.Artists.ToList();
                var artist = artists.Where(a => a.artist_id == artist_id).FirstOrDefault();
                return artist.artist_name;
            }
        }
        public string album_img { get; set; }

        public string new_img
        {
            get
            {
                if (File.Exists(path + album_img))
                {
                    return path + album_img;
                }
                else
                {
                    return just_path + "just_img.png";
                }
            }
        }

        public Nullable<System.DateTime> release_date { get; set; }
        public string new_release_date
        {
            get
            {
                return release_date.Value.ToString("d");
            }
        }
        public Nullable<int> genre_id { get; set; }
        public string genre
        {
            get
            {
                var genres = App.Context.Genres.ToList();
                var genre = genres.Where(a => a.genre_id == genre_id).FirstOrDefault();
                return genre.name;
            }
        }
        public Nullable<int> label_id { get; set; }
        public string label_name
        {
            get
            {
                var labels = App.Context.Labels.ToList();
                var label = labels.Where(a => a.label_id == label_id).FirstOrDefault();
                return label.label_name;
            }
        }
        public Nullable<int> type_id { get; set; }
        public string type_name
        {
            get
            {
                var types = App.Context.Types.ToList();
                var type = types.Where(a => a.type_id == type_id).FirstOrDefault();
                return type.name;
            }
        }

        public virtual Genres Genres { get; set; }
        public virtual Labels Labels { get; set; }
        public virtual Types Types { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
