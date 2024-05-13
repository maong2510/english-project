using MediatR;
using Share.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Base
{
    public class BaseQuery : IRequest<ApiResult<bool>>
    {
    }
}
