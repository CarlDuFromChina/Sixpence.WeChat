using AutoMapper;
using Sixpence.Core.Module.DataService;
using Sixpence.Core.Pixabay;
using Sixpence.Core.Store;
using Sixpence.Core.Store.SysFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixpence.Core.Profiles
{
    public class FileProfile : Profile, IProfile
    {
        public FileProfile()
        {
            CreateMap<sys_file, FileInfoModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(e => e.id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(e => e.name))
                .ForMember(dest => dest.downloadUrl, opt => opt.MapFrom(e => e.DownloadUrl));
        }
    }
}
