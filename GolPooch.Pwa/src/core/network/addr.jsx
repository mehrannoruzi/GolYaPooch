﻿import config from "../../config";

export default {
    auth_getCode: `${config.apiGateway.URL}Authentication/GetCode`,
    auth_verifyCode: `${config.apiGateway.URL}Authentication/VerifyCode`,
    getProfile: `${config.apiGateway.URL}User/Profile`,
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
    deliverNotification: (id) => `${config.apiGateway.URL}Notification/AddDelivery?notificationId=${id}`,
    getTickets: (pageSize, pageNumber) => `${config.apiGateway.URL}Ticket/Top?pageSize=${pageSize}&pageNumber=${pageNumber}`,
    getUnReadTicketCount: `${config.apiGateway.URL}Ticket/UnReadCount`,
    readTicket: (id) => `${config.apiGateway.URL}Ticket/Read?ticketId=${id}`,
    addTicket: `${config.apiGateway.URL}Ticket/Add`,
    getActivePurchase: (pageSize, pageNumber) => `${config.apiGateway.URL}Purchase/Active?pageSize=${pageSize}&pageNumber=${pageNumber}`,
    getAllPurchase: (pageSize, pageNumber) => `${config.apiGateway.URL}Purchase/All?pageSize=${pageSize}&pageNumber=${pageNumber}`,
    spendChance: `${config.apiGateway.URL}chest/SpendChance`,
    getMyChanceCount: (chestId) => `${config.apiGateway.URL}chest/MyChanceCount?chestId=${chestId}`,
    purchaseProduct: `${config.apiGateway.URL}Payment/Create`,
    getLatestWinners: (pageSize, pageNumber) =>`${config.apiGateway.URL}RoundWinner/LastWinners?pageSize=${pageSize}&pageNumber=${pageNumber}`,
    getMostWinners: (pageSize, pageNumber) =>`${config.apiGateway.URL}RoundWinner/MustWinners?pageSize=${pageSize}&pageNumber=${pageNumber}`
}