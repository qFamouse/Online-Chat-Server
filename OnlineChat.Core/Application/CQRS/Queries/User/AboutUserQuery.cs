﻿using Contracts.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.User
{
    public class AboutUserQuery : IRequest<AboutUserView>
    {
        public AboutUserQuery() { }
    }
}