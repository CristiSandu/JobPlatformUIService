﻿using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ValidateJobModelRequest : IRequest<bool>
{
    public bool IsCheck { get; set; }
    public string JobId { get; set; }

}
