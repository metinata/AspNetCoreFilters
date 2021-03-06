﻿using System;
using System.Collections.Generic;
using AspNetCoreFilters.Filters.ActionFilters;
using AspNetCoreFilters.Filters.ExceptionFilters;
using AspNetCoreFilters.Filters.ResourceFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFilters.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomTwoLoggingExceptionFilter))]
    public class TestExceptionController : Controller
    {
        private readonly ILogger _logger;

        public TestExceptionController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("TestExceptionController");
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomOneLoggingExceptionFilter))]
        [ServiceFilter(typeof(CustomOneResourceFilter))]
        [ServiceFilter(typeof(ConsoleLogActionTwoFilter))]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Executing Http Get before exception");
            throw new Exception("Yes a great exception");
        }

        [HttpGet("getwithorder")]
        [ServiceFilter(typeof(CustomOneLoggingExceptionFilter), Order = -1)]
        [ServiceFilter(typeof(CustomOneResourceFilter))]
        public IEnumerable<string> GetWithOrderedFiltered()
        {
            _logger.LogInformation("Executing Http Get before exception");
            throw new Exception("Yes a great exception");
        }

        [HttpGet("getcustomexception")]
        [ServiceFilter(typeof(CustomOneLoggingExceptionFilter))]
        [ServiceFilter(typeof(CustomOneResourceFilter))]
        public IEnumerable<string> GetWithCustomException()
        {
            _logger.LogInformation("Executing Http Get before exception");
            throw new CustomException("Yes a great exception");
        }

        [HttpGet("getnoexception")]
        [ServiceFilter(typeof(CustomOneLoggingExceptionFilter))]
        [ServiceFilter(typeof(CustomOneResourceFilter))]
        public IEnumerable<string> GetNoException()
        {
            _logger.LogInformation("Executing Http GetNoException");
            return new string[] { "test data one", "test data two" };
        }

    }
}
