using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace JupiterApp.Data
{
    public interface IRowConverter<T> where T : IRow
    {
        T ConvertRow(IMapper mapper);
    }

    public class Album : IRowConverter<AlbumRow>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public AlbumGenre Genre { get; set; }

        public AlbumRow ConvertRow(IMapper mapper)
        {
            var row = mapper.Map<AlbumRow>(this);
            row.Original = mapper.Map<Album>(this);

            return row;
        }
    }

    public interface IRow { }

    public class Row<T> : IRow
    {
        public T Original { get; set; }
        public RowState State { get; set; }
        public RowMode Mode { get; set; }
    }

    public class AlbumRow : Row<Album>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public AlbumGenre Genre { get; set; }
    }

    public enum AlbumGenre
    {
        [Display(Name ="Rock")]
        Rock = 0,
        [Display(Name = "Classic")]
        Classic = 1,
        [Display(Name = "Pop")]
        Pop = 2,
        [Display(Name = "R&B")]
        RnB = 3,
        [Display(Name = "Hip Hop")]
        HipHop = 4
    }

    public enum RowState
    {
        None,
        Added,
        Edited,
        Deleted
    }

    public enum RowMode
    {
        None,
        Editing,
        Deleting
    }
}

namespace JupiterApp
{
    public static class EnumExtensions
    {
        public static string GetEnumDisplayName(this Enum value)
        {
            return value.GetType().GetField(value.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name;
        }
        public static int GetEnumValue(this Enum value)
        {
            return (int)value.GetType().GetField(value.ToString()).GetValue(value);
        }
    }
}
