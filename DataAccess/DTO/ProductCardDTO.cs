namespace MscApi.DataAccess.DTO;

public record ProductCardDTO
(
    string Name,
    string Model,
    string Brand,
    string Description,
    uint Price,
    string ImgPath,
    uint Discount
);