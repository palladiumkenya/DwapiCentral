﻿using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Shared.Domain.Model.Common;
using DwapiCentral.Shared.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Commands;

public class VerifySubscriber : IRequest<VerificationResponse>
    {
        public string DocketId { get; set; }
        public string SubscriberId { get; }
        public string AuthToken { get; }

        public VerifySubscriber(string docketId, string subscriberId, string authToken)
        {
            DocketId = docketId;
            SubscriberId = subscriberId;
            AuthToken = authToken;
        }
    }

public class VerifySubscriberHandler : IRequestHandler<VerifySubscriber, VerificationResponse>
{
    private readonly IDocketRepository _repository;

    public VerifySubscriberHandler(IDocketRepository repository)
    {
        _repository = repository;
    }


    public async Task<VerificationResponse> Handle(VerifySubscriber request, CancellationToken cancellationToken)
    {
        var docket = await _repository.GetDocketId(request.DocketId);

        if (null == docket)
            throw new DocketNotFoundException(request.DocketId);

        if (!docket.SubscriberExists(request.SubscriberId))
            throw new SubscriberNotFoundException(request.SubscriberId);

        if (docket.SubscriberAuthorized(request.SubscriberId, request.AuthToken))
            return new VerificationResponse(docket.Name, true);

        throw new SubscriberNotAuthorizedException(request.SubscriberId);
    }
}

