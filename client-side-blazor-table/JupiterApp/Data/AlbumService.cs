using AutoMapper;
using MFramework.Services.FakeData;
using System.Collections.Generic;
using System.Linq;

namespace JupiterApp.Data
{
    public interface IAlbumService
    {
        List<AlbumRow> GetAlbumRows();
        List<Album> GetAlbums();
        void ResetAlbum(AlbumRow albumRow);
        void Save(List<AlbumRow> albumRows);
    }

    public class AlbumService : IAlbumService
    {
        private static List<Album> _albums;
        private readonly IMapper _mapper;

        public AlbumService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Save(List<AlbumRow> albumRows)
        {
            // Deleting to deleted rows.
            _albums.RemoveAll(x => albumRows.Where(y => y.State == RowState.Deleted).Select(y => y.Id).Contains(x.Id));

            // Adding to added rows.
            _albums.AddRange(albumRows.Where(x => x.State == RowState.Added).Select(x => _mapper.Map<Album>(x)).ToList());

            // Updating to edited rows.
            albumRows.Where(row => row.State == RowState.Edited).ToList()
                .ForEach(row => _mapper.Map(row, _albums.SingleOrDefault(y => y.Id == row.Id)));
        }

        public List<Album> GetAlbums()
        {
            if (_albums == null)
            {
                _albums = new List<Album>();

                for (int i = 0; i < 10; i++)
                {
                    _albums.Add(new Album
                    {
                        Id = i,
                        Name = NameData.GetCompanyName(),
                        Desc = TextData.GetSentence(),
                        Genre = EnumData.GetElement<AlbumGenre>()
                    });
                }
            }

            return _albums;
        }

        public List<AlbumRow> GetAlbumRows()
        {
            return GetAlbums().Select(x => x.ConvertRow(_mapper)).ToList();
        }

        public void ResetAlbum(AlbumRow albumRow)
        {
            _mapper.Map(albumRow.Original, albumRow);
            albumRow.State = RowState.None;
        }
    }
}
