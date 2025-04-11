using AutoMapper;
using SistemaVenta.Model.Models;
using SistemaVenta.DTO;
using System.Globalization;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Auth
            CreateMap<AuthDTO, Usuario>().ReverseMap();
            #endregion

            #region Token
            CreateMap<TokenDTO, Usuario>().ReverseMap();
            #endregion

            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(src => (bool)src.esActivo ? 1 : 0))
                .ForMember(dest => dest.foto, opt => opt.MapFrom(src => src.foto));

            CreateMap<Usuario, SesionDTO>()
                .ForMember(dest => dest.RolDescripcion, opt => opt.MapFrom(src => src.IdRolNavigation != null ? src.IdRolNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.foto));

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.IdRolNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.esActivo, opt => opt.MapFrom(src => src.EsActivo == 1))
                .ForMember(dest => dest.foto, opt => opt.MapFrom(src => src.foto));
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.DescripcionCategoria, opt => opt.MapFrom(src => src.IdCategoriaNavigation != null ? src.IdCategoriaNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => Convert.ToString(src.Precio, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(src => (bool)src.EsActivo ? 1 : 0))
                .ForMember(dest => dest.foto, opt => opt.MapFrom(src => src.Foto));

            CreateMap<ProductoDTO, Producto>()
                .ForMember(dest => dest.IdCategoriaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => Convert.ToDecimal(src.Precio, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(src => src.EsActivo == 1))
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.foto));
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(dest => dest.TotalTexto, opt => opt.MapFrom(src => Convert.ToString(src.Total.GetValueOrDefault(), new CultureInfo("es-EC"))))
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro.HasValue ? src.FechaRegistro.Value.ToString("dd/MM/yyyy") : string.Empty));

            CreateMap<VentaDTO, Venta>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalTexto, new CultureInfo("es-EC"))));
            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(dest => dest.DescripcionProducto, opt => opt.MapFrom(src => src.IdProductoNavigation != null ? src.IdProductoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.PrecioTexto, opt => opt.MapFrom(src => Convert.ToString(src.Precio.GetValueOrDefault(), new CultureInfo("es-EC"))))
                .ForMember(dest => dest.TotalTexto, opt => opt.MapFrom(src => Convert.ToString(src.Total.GetValueOrDefault(), new CultureInfo("es-EC"))));

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => Convert.ToDecimal(src.PrecioTexto, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalTexto, new CultureInfo("es-EC"))));
            #endregion

            #region ReporteVenta
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src =>
                    src.IdVentaNavigation != null && src.IdVentaNavigation.FechaRegistro.HasValue
                        ? src.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")
                        : string.Empty))
                .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src =>
                    src.IdVentaNavigation != null ? src.IdVentaNavigation.NumeroDocumento : string.Empty))
                .ForMember(dest => dest.TipoPago, opt => opt.MapFrom(src =>
                    src.IdVentaNavigation != null ? src.IdVentaNavigation.TipoPago : string.Empty))
                .ForMember(dest => dest.TotalVenta, opt => opt.MapFrom(src =>
                    Convert.ToString(src.IdVentaNavigation != null ? src.IdVentaNavigation.Total.GetValueOrDefault() : 0, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.Producto, opt => opt.MapFrom(src =>
                    src.IdProductoNavigation != null ? src.IdProductoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src =>
                    Convert.ToString(src.Precio.GetValueOrDefault(), new CultureInfo("es-EC"))))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src =>
                    Convert.ToString(src.Total.GetValueOrDefault(), new CultureInfo("es-EC"))));
            #endregion
        }
    }
}
