namespace MscApi.DataAccess.DTO;

public record ProductDTO
(
    uint Id,
    string Name,
    string Model,
    string Brand,
    string Description,
    uint Stock,
    uint Price,
    uint Cost,
    uint Discount,
    bool Featured,
    string ImgPath
);