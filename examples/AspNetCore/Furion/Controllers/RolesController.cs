using System;
using System.Net;
using System.Threading.Tasks;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Exceptions;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        // 正常数据返回
        [HttpGet]
        public ActionResult<string[]> GetAll() => Array.Empty<string>();


        // 返回403
        /*
         * RESTful 风格（RESTfulResultProvider）
         * {
         *   "statusCode": 403,
         *   "data": null,
         *   "successed": false,
         *   "errors": "403 Forbidden",
         *   "extras": null
         * }
         * 
         * Problem+Json 风格（ProblemJsonResultProvider）
         * {
         *   "type": "https://tools.ietf.org/html/rfc7231#section-6.5.3",
         *   "title": "Forbidden",
         *   "status": 403,
         *   "traceId": "00-fd8a90c935b20d45b5367cc181eb34e9-44b94d557bf0a14d-00"
         * }
         */
        [HttpGet("{id}")]
        public IActionResult Get(string id) => StatusCode((int) HttpStatusCode.Forbidden);


        // 返回401
        /*
         * RESTful 风格（RESTfulResultProvider）
         * {
         *   "statusCode": 401,
         *   "data": null,
         *   "successed": false,
         *   "errors": "401 Unauthorized",
         *   "extras": null
         * }
         * 
         * Problem+Json 风格（ProblemJsonResultProvider）
         * {
         *   "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
         *   "title": "Unauthorized",
         *   "status": 401,
         *   "traceId": "00-dce7ada6d019984e84bc3fd7721e2757-9bab940d1bc53948-00"
         * }
         */
        [HttpGet("{id}/users")]
        public IActionResult GetRoleUsers(string id) => StatusCode((int) HttpStatusCode.Unauthorized);


        // 模型验证结果输出
        /*
         * RESTful 风格（RESTfulResultProvider）
         * {
         *   "statusCode": 400,
         *   "data": null,
         *   "successed": false,
         *   "errors": {
         *     "Id": [
         *       "The Id field is required."
         *     ],
         *     "Name": [
         *       "The Name field is required."
         *     ]
         *   },
         *   "extras": null
         * }
         * 
         * Problem+Json 风格（ProblemJsonResultProvider）
         * {
         *   "title": "发生错误",
         *   "status": 400,
         *   "instance": "/api/roles",
         *   "errors": {
         *     "Id": [
         *       "The Id field is required."
         *     ],
         *     "Name": [
         *       "The Name field is required."
         *     ]
         *   }
         * }
         */
        [HttpPost]
        public ActionResult<RoleModel> Post(RoleModel role) => role;


        // 未知错误结果输出
        /*
         * RESTful 风格（RESTfulResultProvider）
         * {
         *   "statusCode": 500,
         *   "data": null,
         *   "successed": false,
         *   "errors": "Specified cast is not valid.",
         *   "extras": null
         * }
         * 
         * Problem+Json 风格（ProblemJsonResultProvider）
         * {
         *   "title": "发生错误",
         *   "status": 500,
         *   "instance": "/api/roles",
         *   "errors": {
         *     "未知异常": [
         *       "Specified cast is not valid."
         *     ]
         *   }
         * }
         */
        [HttpPut]
        public IActionResult Put() => throw new InvalidCastException();


        // 调用Oops.Oh已知错误结果输出
        /*
         * RESTful 风格（RESTfulResultProvider）
         * {
         *   "statusCode": 400,
         *   "data": null,
         *   "successed": false,
         *   "errors": "[RoleNotFound] 该角色“1”不存在",
         *   "extras": {
         *     "code": 200404
         *   }
         * }
         * 
         * Problem+Json 风格（ProblemJsonResultProvider）
         * {
         *   "title": "发生错误",
         *   "status": 400,
         *   "instance": "/api/roles/1",
         *   "errors": {
         *     "未知异常": [
         *       "[RoleNotFound] 该角色“1”不存在"
         *     ]
         *   }
         * }
         */
        [HttpPatch("{id}")]
        // RoleNotFoundException 构造函数需要一个string的参数，来接收message
        public IActionResult Patch(string id) => throw Oops.Oh(ErrorCodes.RoleNotFound, typeof(RoleNotFoundException), id);


        // 已知错误结果输出
        /*
         * RESTful 风格（RESTfulResultProvider）
         * {
         *   "statusCode": 400,
         *   "data": null,
         *   "successed": false,
         *   "errors": "该角色“1”不存在",
         *   "extras": {
         *     "code": 200404
         *   }
         * }
         * 
         * Problem+Json 风格（ProblemJsonResultProvider）
         * {
         *   "title": "发生错误",
         *   "status": 400,
         *   "instance": "/api/roles/1",
         *   "errors": {
         *     "RoleNotFound": [
         *       "该角色“1”不存在"
         *     ]
         *   }
         * }
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) => throw new AppException(ErrorCodes.RoleNotFound, $"该角色“{id}”不存在");
    }
}