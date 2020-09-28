import config from "../../config";

export default {
    auth_getCode: `${config.apiGateway.URL}Authentication/GetCode`,
    auth_verifyCode: `${config.apiGateway.URL}Authentication/VerifyCode`,
    getBanners: `${config.apiGateway.URL}Banner/All`,
    getProducts: `${config.apiGateway.URL}Product/All`,
    uploadAvatar: `${config.apiGateway.URL}User/UploadAwatar`,
    updateProfile: `${config.apiGateway.URL}User/UpdateProfile`,
    getChests: `${config.apiGateway.URL}Chest/All`,
    registerForNotification: `${config.apiGateway.URL}Push/Subscribe`,
    getGateways: `${config.apiGateway.URL}Payment/AllGateway`,
    getNotifications: (pageSize, pageNumber) => `${config.apiGateway.URL}Notification/Top?pageSize=${pageSize}&pageNumber=${pageNumber}`,
}