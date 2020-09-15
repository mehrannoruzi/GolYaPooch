import config from "../../config";

export default {
    auth_getCode: `${config.apiGateway.URL}Authentication/GetCode`,
    auth_verifyCode: `${config.apiGateway.URL}Authentication/VerifyCode`,
    auth_resentCode: `${config.apiGateway.URL}Authentication/ResendCode`,
}