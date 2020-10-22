import http from '../core/network';
import addr from '../core/network/addr';

export default class notificationSrv {

    static async get(pageSize, pageNumber) {
        //return await http.get(addr.getTransactions(pageSize, pageNumber));
        return {
            isSuccessful: true,
            result: {
                items: [
                    {
                        type: 1,
                        productText: 'بسته 6 ماهه فیلیمو',
                        insertDateSh: '1399/08/01',
                        time: '20:08',
                        gatewayName: "بانک پاسارگاد",
                        isSuccess: true,
                        price: 15000
                    },
                    {
                        type: 2,
                        productText: 'بسته 1 ماهه فیلیمو',
                        insertDateSh: '1399/08/01',
                        time: '20:08',
                        gatewayName: "بانک پاسارگاد",
                        isSuccess: true,
                        price: 13000
                    },
                    {
                        type: 2,
                        productText: 'بسته 3 ماهه فیلیمو',
                        inserDateSh: '1399/08/01',
                        time: '20:08',
                        gatewayName: "بانک پاسارگاد",
                        isSuccess: false,
                        price: 17000
                    },
                    {
                        type: 2,
                        productText: 'بسته 3 ماهه فیلیمو',
                        inserDateSh: '1399/08/01',
                        time: '20:08',
                        gatewayName: "بانک پاسارگاد",
                        isSuccess: false,
                        price: 17000
                    },
                    {
                        type: 2,
                        productText: 'بسته 3 ماهه فیلیمو',
                        inserDateSh: '1399/08/01',
                        time: '20:08',
                        gatewayName: "بانک پاسارگاد",
                        isSuccess: false,
                        price: 15000
                    },
                    {
                        type: 2,
                        productText: 'بسته 3 ماهه فیلیمو',
                        inserDateSh: '1399/08/01',
                        time: '20:08',
                        gatewayName: "بانک پاسارگاد",
                        isSuccess: false,
                        price: 17000
                    },

                ]
            }
        };
    }
}