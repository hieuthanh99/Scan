export interface image{
    name: string;

	path: string;

	createdDate:string;

	modifiedDate:string;

	createdByUser: string;

	modifiedByUser: string;

	// Thông tin vĩ độ lấy được từ device
	lat:string;

	// Thông tin kinh độ lấy được từ device
	lng:string;

	coordinatesInfo:string;

	status:string;

	// Trạng thái đồng bộ của ảnh
	sync:boolean;

	// Danh sách các mã phương án đã sử dụng ảnh
	useForPlan:string;
}