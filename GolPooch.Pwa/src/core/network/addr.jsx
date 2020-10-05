﻿import config from "../../config";

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
    getUnReadNotifCount: `${config.apiGateway.URL}Notification/UnReadCount`,
    readNotification: (id) => `${config.apiGateway.URL}Notification/Read?notificationId=${id}`,
    getTickets: (pageSize, pageNumber) => `${config.apiGateway.URL}Ticket/Top?pageSize=${pageSize}&pageNumber=${pageNumber}`,
    getUnReadTicketCount: `${config.apiGateway.URL}Ticket/UnReadCount`,
    readTicket: (id) => `${config.apiGateway.URL}Ticket/Read?ticketId=${id}`,
    getPurchase: (pageSize, pageNumber) => `${config.apiGateway.URL}Purchase/Top?pageSize=${pageSize}&pageNumber=${pageNumber}`,
    spendChance: `${config.apiGateway.URL}chest/SpendChance`,
    getChestInChance: (chestId) => `${config.apiGateway.URL}chest/MyChance?chestId=${chestId}`,
    purchaseProduct: `${config.apiGateway.URL}Payment/Create`,
}