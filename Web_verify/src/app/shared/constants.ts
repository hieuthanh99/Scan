export const D_NUMBER_PHONE = 'numberPhone';
export const D_FREE = 'textFree';
export const D_FREE_TEXT = 'dtextFree';
export const D_TEXT = 'text';
export const D_CURRENCY = 'currency';
export const D_NUMBER = 'number';
export const URL_BASE = '';
export const GENERATE_TOKEN = 'generate-token';
export const GENERATE_OTP = 'save-otp-info';
export const REVOKE_TOKEN = 'revoke-token';
export const VERIFYOTP = 'verify-otp';

export const MAKER = 'MAKER'; // Role nhập liệu


export const PROCESS_STATUS_DELETED = 'D'; // Đã xóa
export const PROCESS_STATUS_PROCESSING = 'E'; // Đang xử lý
export const PROCESS_STATUS_WAITING = 'W'; // Chờ duyệt
export const PROCESS_STATUS_APPROVED = 'A'; // Đã duyệt
export const PROCESS_STATUS_REJECT = 'R'; // Từ chối
export const PROCESS_STATUS_FALSE = 'F'; // Duyệt thất bại
export const PROCESS_STATUS_UPDATE = 'U'; // Yêu cầu bổ sung
export const PROCESS_STATUS_APPROVED_PART = 'P'; // Duyệt một phần
export const PROCESS_STATUS_WAITING_PROCESSING = 'WE'; // Chờ xử lý
export const PROCESS_STATUS_APPROVED_WAITING_ACTIVE = 'AW'; // Duyệt chờ active
export const APPROVER = 'APPROVER'; // Role duyệt

export const USER_NAME = 'USER_NAME';
export const CURRENCY_APP_CODE = 'CURRENCY_APP_CODE';
export const CURRENCY_APP_ID = 'CURRENCY_APP_ID';
export const CURRENCY_KEYCLOAK_ID = 'CURRENCY_KEYCLOAK_ID';
export const CURRENCY_CLIENT_ID = 'CURRENCY_CLIENT_ID';
export const LIST_APP = 'LIST_APP';

export const STATUS = [
    { key: '', value: 'Tất cả trạng thái', class: '' },
    { key: 'E', value: 'Đang xử lý', class: 'wf-status-inprocess' },
    { key: 'W', value: 'Chờ duyệt', class: 'wf-status-waitting' },
    { key: 'A', value: 'Đã duyệt', class: 'wf-status-approved' },
    { key: 'U', value: 'Yêu cầu bổ sung', class: 'wf-status-inprocess' },
    { key: 'P', value: 'Duyệt một phần', class: 'wf-status-halfapprove' },
    { key: 'F', value: 'Duyệt thất bại', class: 'wf-status-halfapprove' },
    { key: 'R', value: 'Từ chối duyệt', class: 'wf-status-reject' },
    { key: 'D', value: 'Hủy', class: 'wf-status-reject' },
];

export const Status = {
    SUCCESS: 0,
    EXIST: 107,
    ERROR: 500,
};

// khai báo các định dạnh button show ở footer
export const TYPE_BTN_FOOTER = {
    TYPE_SAVE: 'SAVE', // lưu thông tin
    TYPE_REFUSE: 'REFUSE', // từ chối
    TYPE_DELETE: 'DELETE', // hủy hồ sơ
    TYPE_UPDATE: 'UPDATE', // cập nhật
    TYPE_CANCEL: 'CANCEL', // hủy
    TYPE_SEND_APPROVER: 'SEND_APPROVER', // gửi duyệt
    TYPE_BACK: 'BACK', // quay lại
    TYPE_APPROVER: 'APPROVER', // duyệt
    TYPE_UNDO: 'UNDO',
    TYPE_RECALL: 'RECALL',
    TYPE_EXIT: 'EXIT', // Thoát
    TYPE_CONTINUE: 'CONTINUE', // Tiếp tục,
    TYPE_QR: 'INQR', // IN QR Code
    TYPE_INIT: 'INIT', // Tạo hồ sơ
    TYPE_SAVE_APPROVE: 'SAVE_APPROVE', // Lưu và duyệt
};

export const BUTTON_DELETE = {
    title: 'Xóa',
    classBtn: 'left-btn btn-light-error btn-border-light-error',
    typeBtn: TYPE_BTN_FOOTER.TYPE_DELETE,
    icon: 'ic-delete',
    status: [PROCESS_STATUS_PROCESSING]
};
export const BUTTON_UNDO = {
    title: 'Quay lại',
    classBtn: 'btn-light-blue',
    typeBtn: TYPE_BTN_FOOTER.TYPE_UNDO,
    icon: 'ic-angle_left',
};
export const BUTTON_SAVE = {
    title: 'Lưu thông tin',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_SAVE,
    status: ['', PROCESS_STATUS_PROCESSING]
};
export const BUTTON_SEND_APPROVER = {
    title: 'Gửi duyệt',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_SEND_APPROVER,
    status: ['', PROCESS_STATUS_PROCESSING]
};
export const BUTTON_APPROVER = {
    title: 'Duyệt',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_APPROVER,
    status: [PROCESS_STATUS_WAITING]
};
export const BUTTON_TYPE_REFUSE = {
    title: 'Từ chối duyệt',
    classBtn: 'btnREFUSE left-btn btn-white-error btn-border-light-error',
    typeBtn: TYPE_BTN_FOOTER.TYPE_REFUSE,
    status: [PROCESS_STATUS_WAITING]
};
export const BUTTON_TYPE_RECALL = {
    title: 'Thu hồi',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_RECALL,
    status: [PROCESS_STATUS_WAITING]
};
export const BUTTON_TYPE_INIT = {
    title: 'Cập nhật gói',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_INIT,
    status: [PROCESS_STATUS_APPROVED]
};
export const BUTTON_TYPE_SAVE_APRROVE = {
    title: 'Xác nhận',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_SAVE_APPROVE,
    status: [PROCESS_STATUS_PROCESSING]
};
export const BUTTON_TYPE_INIT_PROCESS = {
    title: 'Cập nhật hồ sơ',
    classBtn: 'btn-primary',
    typeBtn: TYPE_BTN_FOOTER.TYPE_INIT,
    status: [PROCESS_STATUS_APPROVED]
};

export const ListDashBoard = [
    {
        "id": 1,
        "name": "SmartChannel",
        "url": "http://10.1.27.43:9684",
        "isShowMenu": true,
        "type": 'MFE',
        "subDomain": "smartchannel",
        "image": "assets/images/dform.svg",
        "elementName": "smartchannel-app-element",
        "notes": null,
        "priority": 1,
        "usedStyle": true,

    },
    {
        "id": 200,
        "name": "SM GUIDE",
        "url": "http://10.1.27.141:9702",
        "isShowMenu": true,
        "type": 'MFE',
        "subDomain": "sm_guide",
        "image": "assets/images/b-office.svg",
        "elementName": "sm_guide-app-element",
        "notes": null,
        "priority": 2,
        "usedStyle": true,
    },
    {
        "id": 1,
        "name": "BACKOFFICE",
        "url": "http://10.1.16.211:9200",
        "isShowMenu": true,
        "type": 'IFRAME',
        "subDomain": "retail",
        "image": "assets/images/b-office.svg",
        "elementName": "",
        "notes": null,
        "priority": 2,
        "usedStyle": true,
    },
    {
        "id": 201,
        "name": "Digital Lending",
        "url": "http://10.1.27.129:8821",
        "isShowMenu": true,
        "type": 'MFE',
        "subDomain": "digital_lending",
        "image": "assets/images/dform.svg",
        "elementName": "digital_lending-app-element",
        "notes": null,
        "priority": 3,
        "usedStyle": true,
    },
    // { 
    //   "id": 202, 
    //   "name": "WEB METADATA", 
    //   "url": "http://10.1.16.211:9252", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "web_metadata", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "web_metadata-app-element",
    //   "notes": null,
    //   "priority": 4,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 203, 
    //   "name": "WEB ACMS", 
    //   "url": "http://10.1.16.211:11275", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "appacms", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "appacms-app-element",
    //   "notes": null, 
    //   "priority": 4,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 204, 
    //   "name": "BILATERAL PORTAL", 
    //   "url": "http://10.1.27.141:10711", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "bilateral", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "appacms-app-element",
    //   "notes": null,
    //   "priority": 4,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 205, 
    //   "name": "T24 Retail", 
    //   "url": "http://10.1.27.141:9962", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "t24_retail", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "t24_retail-app-element",
    //   "notes": null,
    //   "priority": 5,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 206, 
    //   "name": "QLCD", 
    //   "url": "http://10.1.27.43:11052", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "qlcd", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "qlcd-app-element",
    //   "notes": null,
    //   "priority": 6,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 207, 
    //   "name": "DIGILANDSTOCK", 
    //   "url": "http://10.215.254.38:8080", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "qlnda", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "qlnda-app-element",
    //   "notes": null,
    //   "priority": 7,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 208, 
    //   "name": "CTV", 
    //   "url": "http://10.215.254.38:8080", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "ctv", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "ctv-app-element",
    //   "notes": null,
    //   "priority": 8,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 208, 
    //   "name": "OPEN BANKING", 
    //   "url": "http://10.1.27.141:10644", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "apimngt", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "apimngt-app-element",
    //   "notes": null,
    //   "priority": 9,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 208, 
    //   "name": "OPEN BANKING", 
    //   "url": "http://10.1.27.141:9702", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "sm_guide", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "sm_guide-app-element",
    //   "notes": null,
    //   "priority": 10,
    //   "usedStyle": true,
    // },
    // { 
    //   "id": 208, 
    //   "name": "PMH", 
    //   "url": "http://10.1.27.129:11233", 
    //   "isShowMenu": true,
    //   "type": 'MFE',
    //   "subDomain": "pmh", 
    //   "image": "assets/images/dform.svg", 
    //   "elementName": "pmh-app-element",
    //   "notes": null,
    //   "priority": 11,
    //   "usedStyle": true,
    // }
];
