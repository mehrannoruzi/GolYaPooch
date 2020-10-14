﻿import React from 'react';

export const validationStrings = {
    required: 'این فیلد اجباری است',
    mustBeNumber: 'این فیلد باید عددی باشد',
    invalidMobileNumber: 'شماره موبایل ورودی صحیح نیست',
    invalidEmail: 'ایمیل اشتباه است',
    passwordInvalidLength: 'گذروازه باید حداقل 5 کاراکتر داشته باشد',
    incorrectRepeatPassword: 'تکرار گذروازه اشتباه است',
    maxFileSizeExceeded: (size) => `حداکثر حجم فایل ${size} مگابایت می باشد`,
    maxFileCountExceeded: (count) => `حداکثر تعداد فایل ها ${count} می باشد`,
    atleastOneFileRequired: 'حداقل یک فایل مورد نیاز است'
};
const strings = {
    appName: 'appName',
    webApp: 'وب اپلیکیشن',
    signInToSystem: 'ورود به سیستم',
    confirm: 'تایید',
    mobileNumber: 'شماره موبایل',
    firstName: 'نام',
    lastName: 'نام خانوادگی',
    email: 'ایمیل',
    region: 'شهر',
    gender: 'جنسیت',
    women: 'زن',
    men: 'مرد',
    birthday: 'تاریخ تولد',
    intruducerId: 'کد معرف',
    edit: 'ویرایش',
    profile: 'پروفایل',
    profileEdit: 'ویرایش اطلاعات کاربری',
    aggreedWithRules: 'با قوانین و مقررات موافقم',
    pleaseEnterVerifyCode: 'لطفا کد تایید پیامک شده را وارد نمایید ',
    unknownError: 'خطایی رخ داده است، لطفا دوباره تلاش نمایید',
    loginHelp: 'لطفاً شماره تلفن همراه‌تان را وارد کنید تا کد فعال‌سازی برای شما ارسال شود.',
    show: 'نمایش',
    ruleAgreementRequired: 'موافقت با قوانین جهت عضویت اجباری می باشد',
    ruelsText: `لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است، چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است، و برای شرایط فعلی تکنولوژی مورد نیاز، و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد، کتابهای زیادی در شصت و سه درصد گذشته حال و آینده، شناخت فراوان جامعه و متخصصان را می طلبد، تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی، و فرهنگ پیشرو در زبان فارسی ایجاد کرد، در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها، و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی، و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.`,
    rules: 'قوانین و مقررات',
    underestood: 'متوجه شدم !',
    resendCodeCountDown: 'ثانیه تا درخواست مجدد کد',
    resentSuccess: 'کد ورود مجددا ارسال گردید',
    changeMobileNumber: 'تغییر شماره تلفن همراه',
    send4Digit: 'کد 4 رقمی به شماره',
    send4Digit_2: 'ارسال شد. کد را اینجا وارد کنید.',
    verifyCode_sendAgain: 'ارسال مجدد کد',
    doesntGiveCode: 'کد فعال‌سازی را دریافت نکردید؟',
    moneyCurrency: 'تومان',
    return: 'بازگشت',
    profileSuccessUpdate: 'تکمیل پروفایل با موفقیت انجام شد',
    addHomeScreen: 'را به موبایل خود اضافه کنید',
    addHomeScreenStep1: <>در نوار بالا روی دکمه <strong>Share</strong> تپ کنید</>,
    addHomeScreenStep2: <>در منوی باز شده، در قسمت پایین، گزینه <strong>Add to Home Screen</strong> را انتخاب کنید</>,
    addHomeScreenStep3: <>در مرجله بعد در قسمت بالا روی <strong>Add</strong> تپ کنید</>,
    purchase: 'خرید',
    gateways: 'برای پرداخت درگاه خود را انتخاب نمایید',
    yourProfit: 'سود شما',
    chance: 'شانس برنده شدن',
    discount: 'تخفیف',
    detailProduct: 'جزییات سفارش',
    logOut: 'خروج',
    pageName_store: 'فروشگاه',
    pageName_activities: 'بسته های من',
    pageName_chest: 'بسته ها',
    pageName_leaderboard: 'برنده ها',
    pageName_setting: 'سایر',
    successfulPayment: 'پرداخت با موفقیت انجام شد',
    failedPayment: 'پرداخت با خطا مواجه شد',
    iAgree: 'قبول می کنم',
    iReject: 'رد می کنم',
    pleaseChangeYourSelectedPackage: 'لطفا بسته انتخابی خود را تغییر دهید',
    supportCenter: 'مرکز پشتیبانی',
    aboutUs: 'درباره ما',
    appVersion: 'نسخه اپلیکیشن',
    thereIsNoNotification: 'هیچ اعلانی وجود ندارد',
    thereIsNoTicket: 'هیچ تیکتی وجود ندارد',
    sendTicket: 'ارسال تیکت',
    pleaseWait: 'لطفا منتظر یمانید',
    thereIsNoActivePackage: 'هیچ بسته فعالی وجود ندارد',
    thereIsNoPackage: 'هیچ بسته ای وجود ندارد',
    select: 'انتخاب'
};
export default strings;