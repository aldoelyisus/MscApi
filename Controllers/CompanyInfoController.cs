using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using MscApi.DataAccess.Repository.Interfaces;
using MscApi.Domain.Entities;

namespace MscApi.Controllers;

[Route("api/about")]
[ApiController]
public class CompanyInfoController : ControllerBase
{
    private readonly ILogger<CompanyInfoController> _logger;
    private readonly IMapper _mapper;
    private readonly ICompanyInfoRepository _repository;

    public CompanyInfoController(ICompanyInfoRepository repository, ILogger<CompanyInfoController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyInformation>>> GetCompanyInfo()
    {
        CompanyInformation info;

        try
        {
            info = await _repository.GetCompanyInfo();

            if(info == null)
                return NotFound();
        }
        catch(Exception)
        {
            return NoContent();
        }

        return Ok(info);
    }
}