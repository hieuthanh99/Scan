using Scan.Core.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Scan.Core.Settings
{
    public enum ApplicationPage
    {
        Login = 0,
        Scan = 1,
        OCRTracking = 2,
        OCRTrackingDetail = 3,
        HomeScan = 4,
        ClassifyScan = 5,
        PreviewScan = 6,
        ScanNoLogin = 7,
        CaptureList = 8,
        SignaturePreview = 9,
        QuickScanLogin = 10,
        VHSScan = 11,
        AutoUpload = 12,
        TSDBScanImport = 13,
        TSDBPreview = 14,
        // luồng KHDN
        KHDNClassifyScan = 15,
        KHDNPreviewScan = 16,
        KHDNSignaturePreview = 17,
        // luồng CCT
        CCTClassifyScan = 18,
        CCTPreviewScan = 19,
        // luồng HSTHE
        HSTHEClassifyScan = 20,
        HSTHEPreviewScan = 21,
        // Tín dụng KHCN
        TDCNClassifyScan = 22,
        TDCNPreviewScan = 23,
        // Tín dụng KHDN
        TDDNClassifyScan = 24,
        TDDNPreviewScan = 25,
        // Tài sản đảm bảo KHDN
        TSDBDNScanImport = 26,
        TSDBDNPreview = 27,
        Home = 28,
        ListCapture = 29,
        AutoUploadV2,
    }

    public enum FileType
    {
        // luồng quầy 
        hd_dichvu_blankpage = 0,
        hd_dichvu_p1 = 1,
        hd_dichvu_signature = 2,
        hd_dichvu_last = 3,

        hd_dichvu_none = 4,

        // Tài sản đảm bảo
        hopdongthechap_p1 = 6,
        bienbanbangiao_p1 = 7,
        bienbandinhgia_p1 = 8,
        chungtuthanhtoan_p1 = 9,
        denghiphongtoa_p1 = 10,
        dkgdbd_p1 = 11,
        giaytosohuu_p1 = 12,
        hosobaohiem_p1 = 13,
        xacnhanphongtoa_p1 = 14,
        hosokhac_p1 = 15,

        // luồng KHDN
        page_dkkd = 16,
        page_dieu_le = 17,
        page_qd_bo_nhiem = 18,
        page_gttt = 19,
        page_phuluc_mb = 20,
        page_hs_hdsd_dichvu = 21,
        page_khdn_hosokhac = 22,

        // chữ ký luồng quầy
        page_hs_nguoi_dai_dien_hp = 23,
        page_hs_nguoi_uy_quyen_tk = 24,
        page_hs_ketoan = 25,
        page_uyquyen_ketoan = 26,
        page_khdn_chuky_khac = 27,

        // luồng chấm chứng từ
        page_chungtu_giayguitietkiem = 28,
        page_chungtu_uy_nhiem_chi = 29,
        page_chungtu_bang_ke = 30,
        page_chungtu_giayrut_tiengui = 31,
        page_chungtu_giayrut_tiengui_chuky = 32,
        page_chungtu_so_tietkiem = 33,
        page_chungtu_so_tietkiem_bang_td_sodu = 34,
        page_chungtu_denghi_tattoan = 35,
        page_chungtu_phieu_rut_lai = 36,
        page_chungtu_denghi_vayvon_full = 37,
        page_chungtu_denghi_vayvon_chuky = 38,
        page_chungtu_phieu_phongtoa = 39,
        page_chungtu_phieu_nhapkho_tsdb = 40,
        page_chungtu_phieu_nhap_ngoaibang = 41,
        page_chungtu_phieu_chuyenkhoan = 42,

        page_chungtu_giay_nop_tien = 43,
        page_chungtu_phieu_thudoi_ngoaite = 44,
        page_chungtu_giay_linh_tien_rut_tien_tutaikhoan = 45,
        page_chungtu_giay_linh_tien_rut_tien_vaycamco = 46,
        page_chungtu_giay_linh_tien_mat = 47,
        page_chungtu_thongbao_hoanthanh_phe_duyet_tren_BPM = 48,
        page_chungtu_phieu_nhan_tien = 49,
        page_chungtu_phieu_chi = 50,
        page_chungtu_giay_receive = 51,

        // part 3
        page_chungtu_hop_dong_tien_gui_1 = 52,
        page_chungtu_hop_dong_tien_gui_2 = 53,
        page_chungtu_phieu_chuyen_khoan_tk = 54,
        page_chungtu_de_nghi_tat_toan_hdtg = 55,
        page_chungtu_phieu_tat_toan_stk = 56,
        page_chungtu_de_nghi_hoan_quy = 57,
        page_chungtu_phieu_chuyen_quy = 58,
        page_chungtu_de_nghi_tiep_quy = 59,
        page_chungtu_de_nghi_hoan_quy_uqvc = 60,
        page_chungtu_lenh_nhan_quy_uqvc = 61,
        page_chungtu_de_nghi_tiep_quy_uqvc = 62,
        page_chungtu_lenh_xuat_quy_uqvc = 63,
        page_chungtu_phieu_thu_phi = 64,
        page_chungtu_hop_dong_tien_gui_chu_ky = 65, // trang chữ ký của bộ mở hợp đồng tiền gửi

        // part 4
        page_chungtu_de_nghi_dong_tai_khoan = 66,
        page_chungtu_phieu_tat_toan_tai_khoan = 67,
        page_chungtu_de_nghi_thu_no = 68,
        page_chungtu_phieu_thu_lai_interest = 69,
        page_chungtu_phieu_xuat_kho_tsdb = 70,
        page_chungtu_phieu_xuat_ngoai_bang = 71,
        page_chungtu_tiep_quy_tien_mat_ATM = 72,
        page_chungtu_bien_ban_kiem_quy_may_ATM = 73,
        page_chungtu_bien_lai = 74,
        page_chungtu_nop_de_ghi_vao_taikhoan = 75,
        page_chungtu_thoa_thuan_chuyen_tien = 76,
        page_chungtu_giay_sec = 77,
        page_chungtu_de_nghi_thu_no_chu_ky = 78,
        page_chungtu_giay_sec_chu_ky = 79,
        page_chungtu_de_nghi_tat_toan_hdtg_chu_ky = 80,

        // update ycdt
        page_chungtu_de_nghi_hoan_quy_type_2 = 81,  // Giấy đề nghị hoàn quỹ mẫu 2
        page_chungtu_phieu_thu,

        // part 6
        page_chungtu_rut_mot_phan_goc = 83,

        // luồng hồ sơ thẻ
        page_hsthe_dang_ky_phat_hanh_the = 90,
        page_hsthe_dang_ky_phat_hanh_the_to_2 = 91,

        // luồng hồ sơ tín dụng - KHCN
        // Khế ước (LD)
        page_vb_nhan_no,
        page_vb_nhan_no_vbsd_bo_sung,
        page_vb_sua_doi_bs_van_kien,
        page_de_nghi_giai_ngan_kiem_khe_uoc,
        page_vb_nhan_no_kiem_phu_luc,
        // Báo cáo đề xuất (LD và MD)
        page_bao_cao_de_xuat,
        page_bao_cao_danh_gia_de_xuat,
        page_de_xuat_giai_ngan,
        page_bao_cao_cap_tin_dung,
        // Hồ sơ phê duyệt (LD và MD)
        page_thong_bao_phe_duyet,
        // Ủy nhiệm chi (LD)
        page_unc,
        page_giay_nop_tien_vao_nsnn,
        // Giấy lĩnh tiền mặt (LD)
        page_giay_linh_tien_mat,
        // Hồ sơ chứng minh mục đích (LD)
        page_hoa_don,
        page_hoa_don_gtgt,
        // Hợp đồng cho vay (LD)
        page_hop_dong_cho_vay,
        page_hop_dong_cap_han_muc_thau_chi,
        page_hop_dong_cho_vay_bao_dam,
        page_vb_sua_doi_bs_van_kien_hdcv,
        page_hop_dong_cap_tin_dung,
        page_thoa_thuan_chung,
        // Hồ sơ kiểm soát sau (LD)
        page_bien_ban_kss,
        // Hồ sơ khác (LD và MD)
        page_cam_ket_dong_tra_no,
        page_to_khai_thue_gtgt,
        page_bb_ban_giao_hskh_sme,
        page_bb_ban_giao_hs_khcn,
        page_bb_ban_giao,
        page_bien_ban_ban_giao_hskn_cib,

        // Thư bảo lãnh (MD)
        page_thu_bao_lanh_hoan_tra_tien_ung_truoc,
        page_thu_bao_lanh_thanh_toan,
        page_thu_bao_lanh_thuc_hien_hd,
        page_bao_lanh_thuc_hien_hd,
        page_thu_bao_lanh_vay_von,
        page_bao_lanh_hoan_tra_tien_ung_truoc,
        page_bao_lanh_tien_tam_ung,
        page_thu_bao_lanh_du_thau,
        page_thu_bao_lanh_bao_dam_clsp,
        page_thu_bao_lanh_doi_ung,
        page_thu_xac_nhan_bao_lanh,
        page_xac_nhan_cung_cap_tin_dung_so,
        page_thu_bao_lanh_bao_hanh,
        page_bao_lanh_thanh_toan,
        page_bao_lanh_du_thau,
        page_vb_sua_doi_bao_lanh_du_thau,
        page_vb_sua_doi_bao_lanh_thanh_toan,
        page_vb_sua_doi_bao_lanh_bao_hanh,
        page_vb_sua_doi_bao_lanh_doi_ung,
        page_vb_sd_bl_hoan_tra_tien_ung_truoc,
        page_vb_sd_bl_thuc_hien_hop_dong,
        page_vb_sua_doi_bao_lanh_vay_von,
        page_vb_sd_thu_bl_thuc_hien_hop_dong,
        page_vb_sd_bl_tien_tam_ung,
        page_vb_sd_thu_xac_nhan_cung_cap_td,
        page_vb_sd_xac_nhan_cung_cap_td,
        page_vb_sd_bl_bao_dam_clsp,
        // Đơn đề nghị (MD)
        page_don_dn_cap_bao_lanh,
        page_de_nghi_phat_hanh_bao_lanh,
        page_de_nghi_phat_hanh_bao_lanh_doi_ung,
        page_de_nghi_phat_hanh_xac_nhan_bao_lanh,
        page_de_nghi_sd_bl,
        page_de_nghi_phat_hanh_bl_kiem_vb_sd_bs,
        page_de_nghi_phat_hanh_xncctd,
        page_de_nghi_sua_doi_xncctd,
        page_dn_thuc_hien_giao_dich,
        page_dn_thuc_hien_gd_vb_sua_doi,
        page_don_de_nghi_cap_xn_cc_td,
        // Hợp đồng bảo lãnh (MD)
        page_dn_cap_bao_lanh_kiem_hd_cap_bao_lanh,
        page_hop_dong_cap_bao_lanh,
        page_vb_sua_doi_bs_van_kien_hdbl,

        // Trang đặc biệt cho báo cáo đề xuất Tín dụng KHDN
        page_bao_cao_de_xuat_page_n,

        // luồng KHCN (cập nhật các biểu mẫu mới)
        hskh_hd_dichvu_mau_1_p1,
        hskh_hd_dichvu_mau_1_signature,
        hskh_hd_dichvu_mau_2_p1,
        hskh_hd_dichvu_mau_2_signature,
        hskh_hd_dichvu_mau_3_p1,
        hskh_hd_dichvu_mau_3_signature,
        hskh_thay_doi_tt,
        hskh_thay_doi_tt_page_sau,
        hskh_bs_dich_vu,
        hskh_dong_tai_khoan,
        // dùng trong trường hợp người dùng chọn chữ ký
        hskh_manual_signature,
        // luồng KHDN - bổ sung các biểu mẫu mới
        // thay đổi thông tin
        page_hskhdn_de_nghi_tdtt_1,
        page_hskh_de_nghi_tdtt_cnlq,    // thông tin chữ ký
        page_hskh_de_nghi_tdtt_cn,      // thông tin chữ ký
        // bổ sung dịch vụ
        page_hskhdn_denghi_bs_dv_mau1_p1,
        // đóng tài khoản
        page_hskhdn_denghi_dongtk,
        // Mẫu mở tài khoản chung KHDN-KHDN
        page_hskhdn_denghi_motk_mau1_p1,
        page_hskhdn_denghi_motk_mau1_sign,
        // Mẫu mở tài khoản chung KHCN-KHDN
        page_hskhdn_denghi_motk_mau2_p1,
        page_hskhdn_denghi_motk_mau2_sign,
        page_hskhdn_denghi_motk_res_code, // thông tin lưu giấy phép doanh nghiệp

        // Tài sản đảm bảo KHDN
        // HOPDONGTHECHAP
        page_thuc_hien_gd_vb_sua_doi,
        page_hop_dong_cam_co,
        page_hop_dong_cam_co_tai_san,
        page_hop_dong_cap_tin_dung_hdbd,
        page_hop_dong_the_chap,
        page_hop_dong_the_chap_trang_bia,
        page_pl_hd_the_chap,
        page_pl_hd_the_chap_hang_hoa,
        page_pl_hd_the_chap_hh_quyen_doi_no,
        //page_vb_sua_doi_bs_van_kien,  // đã có ở tín dụng
        page_vb_sua_doi_bs_van_kien_phu_luc,

        // BIENBANBANGIAO
        page_bien_ban_ban_giao,     //  BIÊN BẢN BÀN GIAO
        page_bien_ban_giao_nhan_tsbd,
        page_bien_ban_giao_nhan_hs_ct,
        page_phieu_kiem_soat_cv,
        page_phieu_luan_chuyen_hs,

        // BIENBANDINHGIA
        page_bien_ban_xac_dinh_tsbd,
        page_bien_ban_xac_dinh_lai_tsbd,

        // CHUNGTUTHANHTOAN
        page_giay_nop_tien,
        page_giay_nop_tien_nsnn,
        page_giay_xac_nhan,
        page_giay_xac_nhan_nop_tien,
        page_phieu_bao_co,
        page_phieu_bao_no,
        page_chung_tu_ke_toan,
        page_de_nghi_xac_nhan,

        // DENGHIPHONGTOA
        page_bang_ke_chung_khoan,
        page_de_nghi_thu_tuc_giai_toa,
        page_giay_de_nghi,
        page_de_nghi_ptck_ben_bao_dam,
        page_de_nghi_ptck_tsbd,
        page_thong_bao,                 // THÔNG BÁO
        page_yeu_cau_chuyen_khoan,

        // DKGDBD
        page_bang_ke_chung_khoan_de_nghi,
        page_don_yeu_cau_dang_ky,
        page_giay_de_nghi_xac_nhan,
        page_he_thong_dang_ky,
        page_phieu_yeu_cau_dang_ky_bpbd,
        page_phieu_yeu_cau_dang_ky_bpbd_hop_dong,
        page_phieu_yeu_cau_dang_ky_thay_doi,
        page_phieu_yeu_cau_dang_ky_the_chap,
        page_phieu_yeu_cau_thong_bao,
        page_phu_luc_thong_tin,
        page_vb_chung_nhan_dang_ky,
        page_vb_xac_nhan_dang_ky_bpbd,
        page_vb_xac_nhan_thay_doi_bpbd,

        // GIAYTOSOHUU
        page_hop_dong_giaytosohuu,
        page_hop_dong_cung_cap_thuc_pham,
        page_hop_dong_kinh_te,
        page_hop_dong_mua_ban,
        page_hop_dong_mua_ban_hang_hoa,
        page_hop_dong_mua_ban_thiet_bi_y_te,
        page_hop_dong_nguyen_tac,
        page_hop_dong_thi_cong,
        page_hop_dong_thue_he_thong,
        page_hop_dong_tong_dai_ly,
        page_purchase_contract,
        page_sales_purchase_contract,
        page_sales_agreement,
        page_sales_contract,
        page_xac_nhan_don_hang,
        page_annex_contract,
        page_bao_cao_so_du_chung_khoan,
        page_bao_cao_tong_hop_so_du,
        page_bb_nghiem_thu,                 // BIÊN BẢN NGHIỆM THU
        page_bb_thanh_ly,
        page_chung_nhan_dang_ky_ro_mooc,
        page_chung_nhan_dang_ky_xe_o_to,
        page_contract,
        page_don_dat_hang,
        page_don_dat_hang_mua_xe,
        page_giay_chung_nhan,
        page_giay_chung_nhan_dang_ky_dau_tu,
        page_giay_dk_phuong_tien_thuy_noi_dia,
        page_giay_dk_xe_may_chuyen_dung,
        page_giay_chung_nhan_gop_von,
        page_giay_chung_nhan_kiem_dinh_thiet_bi,
        page_giay_chung_nhan_phan_von_gop,
        page_giay_cn_su_dung_dat,
        page_giay_cn_su_dung_dat_so_huu_nha,
        page_giay_cn_so_huu_co_phan,
        page_giay_cn_so_huu_trai_phieu,
        page_giay_hen,
        page_giay_phep_xay_dung,                    // GIẤY PHÉP XÂY DỰNG
        page_giay_xac_nhan_letter,
        page_giay_xac_nhan_so_du_tk,
        page_giay_xac_nhan_so_du_tien,
        page_giay_xac_nhan_thong_tin,
        //page_hoa_don_gtgt,      // đã có ở tín dụng       // HOÁ ĐƠN GIÁ TRỊ GIA TĂNG
        page_hop_dong,
        page_hop_dong_cho_thue,
        page_export_sales_contract,
        page_giay_cam_ket_giaytosohuu,
        page_hop_dong_cho_thue_lai_dat,
        page_hop_dong_mua_ban_can_ho,
        page_hop_dong_mua_ban_thi_cong,
        page_hop_dong_thue_csht,
        page_hop_dong_thue_dat_giaytosuhuu,                 // HỢP ĐỒNG THUÊ ĐẤT
        page_hop_dong_thue_lai_dat_csht,
        page_hop_dong_thue_lai_su_dung_dat,
        page_hop_dong_tien_gui,
        page_hop_dong_tien_gui_co_ky_han,
        page_phu_luc_hop_dong,
        page_phu_luc_hop_dong_kinh_te,
        page_phu_luc_hop_dong_mua_ban,
        page_phu_luc_hop_dong_thue_dat,
        page_quyet_dinh,                                    // QUYẾT ĐỊNH
        page_purchase_order,
        page_sao_ke_so_du_chung_khoan,
        page_sao_ke_tai_khoan_gd,
        page_so_tiet_kiem,
        page_the_tiet_kiem,
        page_the_tiet_kiem_ky_han,
        page_trang_bo_sung_chung_nhan,
        page_van_ban_cam_ket,
        page_xac_nhan_so_du,
        page_xac_nhan_so_du_tai_khoan,
        page_hop_dong_chuyen_nhuong,
        page_vb_chuyen_nhuong_hop_dong,
        page_hop_dong_mb_can_ho_chungcu,
        page_hop_dong_mua_ban_trai_phieu,
        page_hop_dong_thue_kho,
        page_ban_de_nghi_thanh_toan,
        page_giay_cn_so_huu_nha_su_dung_dat,
        page_hop_dong_mua_ban_nha_o,
        page_thoa_thuan_giaytosohuu,
        page_thu_bao_lanh,
        page_vb_thoa_thuan,
        page_xac_nhan_cua_chu_dau_tu,
        page_bb_kiem_tra_hang_ton_kho,
        page_tong_hop_xuat_nhap_ton,
        page_thong_bao_gtsh,

        // GIAYTOSOHUUKHAC
        page_bb_ban_giao_hang_hoa,
        page_bb_giao_hang,
        page_bb_giao_nhan_hang,
        page_bb_giao_nhan_hang_hoa,
        page_bien_ban_ban_giao_nghiem_thu_may,
        page_bien_ban_ban_giao_may,
        page_bien_ban_nghiem_thu_hoan_thanh,
        page_bien_ban_nghiem_thu_may_moc,
        page_bb_nghiem_thu_thiet_bi,
        page_comercial_invoice,
        page_don_xac_nhan_tinh_trang_nha_o,
        page_bb_nghiem_thu_dua_vao_su_dung,
        page_cam_ket_ba_ben,
        page_cam_ket_hop_tac,
        page_giay_de_nghi_giu_ho_giay_to,
        page_giay_uy_quyen,
        page_giay_xac_nhan_tinh_trang_hon_nhan,
        page_giay_xac_nhan_viec_su_dung_dat,
        page_hop_dong_cho_tang,
        page_hop_dong_tang_cho_quyen_su_dung_dat,
        page_hop_dong_hop_tac_3_ben,
        page_hop_dong_lien_ket_xu_ly_tsbd,
        page_hop_dong_thue_quyen_su_dung_dat,
        page_hop_dong_uy_quyen,
        ////page_thoa_thuan_gtsh_khac,
        page_thoa_thuan_quan_ly_tsbd,
        page_to_khai_hang_hoa_nhap_khau,
        page_vn_cam_ket_tai_san_rieng,
        page_vb_cam_ket_ts_rieng_vo_chong,
        page_vb_thoa_thuan_ba_ben,
        page_vb_thoa_thuan_phan_chia_di_san,
        page_vb_xac_nhan_tai_san_rieng,
        page_vb_xac_nhan_ve_tai_san,
        page_vb_khai_nhan_di_san,
        page_vb_phong_toa_tai_san,
        page_vb_thoa_thuan_ve_ts_khi_ly_hon,
        page_bien_ban_ban_giao_may_moc,
        page_bb_hoan_thanh_lap_dat,
        page_cn_chat_luong_gtsh_khac,
        page_bb_nghiem_thu_ban_giao_su_dung,
        page_bb_xac_nhan_thoi_gian_ht,
        page_don_xin_xac_nhan_nhan_khau,
        page_phu_luc_gtsh_khac,
        page_phu_luc_danh_sach_uy_quyen,

        //HOSOBAOHIEM
        page_cargo_insurance_policy,
        page_danh_muc_tai_san,
        page_danh_muc_tai_san_bao_hiem_bb,
        page_danh_muc_tai_san_duoc_bao_hiem,
        page_danh_sach_tau_tham_gia_bh,
        page_don_bao_hiem_than_tau,
        page_giay_chung_nhan_bao_hiem,
        page_giay_cn_bh_chay_no,
        page_giay_cn_bh_chay_no_rui_ro_ts,
        page_giay_cn_bh_hoa_hoan,
        page_giay_cn_bao_hiem_may_moc,
        page_giay_cn_bao_hiem_may_moc_thiet_bi,
        page_giay_cn_bao_hiem_nha_tu_nhan,
        page_giay_cn_bao_hiem_tau_thuyen,
        page_giay_cn_bao_hiem_tu_nguyen_o_to,
        page_giay_xac_nhan_tinh_hieu_luc_hdbh,
        page_giay_xac_nhan_tinh_hieu_luc_phieu_thu,
        page_giay_yeu_cau_bao_hiem_hang_hoa,
        page_giay_yeu_cau_chuyen_quyen_nha_tu_nhan,
        page_giay_yeu_cau_chuyen_quyen_xe_co_gioi,
        page_hop_dong_bao_hiem_chay_no_bb,
        page_hop_dong_bh_chay_no_bb_rui_ro_ts,
        page_hop_dong_bh_do_vo_may_moc,
        page_hop_dong_bh_hoa_hoan_rui_ro_db,
        page_hop_dong_bh_may_moc_thiet_bi,
        page_hop_dong_bh_nha_tu_nhan,
        page_hop_dong_bh_than_tau_chu_tau_thuy_noi_dia,
        page_hop_dong_bh_than_tau_thuy_noi_dia,
        page_hop_dong_bh_than_tau_trach_nhiem_dan_su,
        page_hop_dong_bh_xe_co_gioi,
        page_hop_dong_hoa_hoan_va_rui_ro_db,
        page_hop_dong_nguyentac_bh_hoa_hoan,
        page_xac_nhan_chuyen_quyen_thu_huong_bh,
        page_giay_yeu_cau_bh_may_moc,

        //HOSOKHAC
        page_bb_hop_hoi_dong_quan_tri,
        page_thong_bao_the_chap_hs_khac,
        page_vb_uy_quyen_hs_khac,
        //page_bao_cao_de_xuat, // đã có ở tín dụng
        page_bao_cao_ket_qua_tu_van,
        page_bb_hop_hoi_dong_thanh_vien,
        page_ket_qua_tu_van_xac_thuc,
        page_to_trinh_nhan_tsbd,

        // bổ sung trường đánh dấu là trang trắng 
        page_blankpage,
    }

    public enum ScanCapa
    {
        FLAT = 1,
        FEED = 2,
        DUP = 4
    }

    public enum NotifyType
    {
        Success = 1,
        Information = 2,
        Warning = 3,
        Error = 4
    }

    public enum BatchStatus
    {
        Local = 0,
        Capture = 1
    }

    public enum DpiResolution
    {
        // http://product.corel.com/help/Designer/540224261/Main/EN/Doc/wwhelp/wwhimpl/common/html/wwhelp.htm?context=Corel_DESIGNER_Help&file=Corel-DESIGNER-Changing-the-dimensions-and-resolution-of-bitmaps.html
        OCR = 200,
        VHS = 100,
        VIEW = 96,
        SIGNATURE = 96
    }

    public enum BusinessTypeApp
    {
        HSKH = 1,
        VHS = 2,
        TSDB = 3,
        KHDN = 4,
        CCT = 5,
        HSTT = 6,
        TDKHCN = 7,
        TDKHDN = 8,
        TSDBDN = 9,
        DVK = 99,
    }

    public enum CaptureStatus
    {
        NEW = 0,
        COMPLETE = 1
    }

    public enum RoleName
    {
        flow_cn_hskh_quay,
        flow_cn_hskh_vhs,
        flow_dn_hskh_quay,
        flow_cn_hstd_hsdb,
        flow_cn_hsthe,
        flow_common_auto_upload,
        flow_common_cct,
        flow_common_auto_upload_hst,
        flow_common_auto_upload_hsns
    }

    public enum OcrBusinessCode
    {
        CHUNGTU_SAN,
    }

    public class AppConfig
    {
        protected AppConfig() { }
        public const string dateFormat = "dd/MM/yyyy HH:mm:ss";
        public const string PDFSeachableFolderName = "PDF2Layer";
        public const string ScanFolderName = "Scan";
        public const string ImportFolderName = "Import";
        public const string ScanNoLoginTemp = "ScanNoLoginTemp";
        public const string AutoUploadLog = "AutoUploadLog";

        // config server info
        // public static readonly string CaptureAPI = "https://uatsmartscan.mbbank.com.vn/";
        // public static readonly string CaptureApiVersion2 = "http://10.1.27.129:9933/";
        // public static readonly string AuthAPI = "https://keycloak-internal-uat.mbbank.com.vn/";
        //
        // public static readonly string RealmName = "internal";
        //
        // public static readonly string CaptureAPI = "http://smart-dev.smartup.com.vn:10635/";
        // public static readonly string CaptureApiVersion2 = "http://smart-dev.smartup.com.vn:9933/";
        // public static readonly string AuthAPI = "http://smart-dev.smartup.com.vn:8082/";
        public static readonly string CaptureAPI = "http://api-gateway.smartsolutionvn.net:56789/smart-scan-mcrs-v1";
        public static readonly string CaptureApiVersion2 = "http://api-gateway.smartsolutionvn.net:56789/smart-scan-mcrs-v2";
        public static readonly string AuthAPI = "http://keycloak.smartsolutionvn.net:8000";

        public static readonly string RealmName = "ecm";
        //public static readonly string OcrServiceUrl = null;
        public static readonly string ClientSecret = null;
        public static readonly string ClientId = ConfigurationManager.AppSettings["ClientId"].ToString();
        public static readonly string GrantType = ConfigurationManager.AppSettings["GrantType"].ToString();
        //public static readonly string EnvPythonPath = ConfigurationManager.AppSettings["EnvPythonPath"].ToString();
        //public static readonly string PythonOCRPath = ConfigurationManager.AppSettings["PythonOCRPath"].ToString();
        //public static readonly string EnvPythonUploadPath = ConfigurationManager.AppSettings["EnvPythonUploadPath"].ToString();
        //public static readonly string AutoScanPath = ConfigurationManager.AppSettings["AutoScanPath"].ToString();
        //public static readonly string AutoUploadPath = ConfigurationManager.AppSettings["AutoUploadPath"].ToString();
        //public static readonly string ServerUpload = ConfigurationManager.AppSettings["ServerUpload"].ToString();
        //public static readonly string GhostScipt = ConfigurationManager.AppSettings["GhostScipt"].ToString();
        //public static readonly string Tesseract = ConfigurationManager.AppSettings["Tesseract"].ToString();
        //public static readonly string UpdateVersionFilePath = ConfigurationManager.AppSettings["UpdateVersionFolderPath"].ToString();
        //public static readonly int ThreadOCRDefault = int.Parse(string.IsNullOrEmpty(ConfigurationManager.AppSettings["ThreadOCRDefault"]) ? "4" : ConfigurationManager.AppSettings["ThreadOCRDefault"]);
        //public static readonly int ThreadPushFileDefault = int.Parse(string.IsNullOrEmpty(ConfigurationManager.AppSettings["ThreadPushFileDefault"]) ? "5" : ConfigurationManager.AppSettings["ThreadPushFileDefault"]);

#pragma warning disable S2386 // Mutable fields should not be "public static"
        public static readonly Dictionary<ApplicationPage, string> TitileApp = new Dictionary<ApplicationPage, string>() {
            { ApplicationPage.Login, "Đăng Nhập" },
            { ApplicationPage.HomeScan, "TẠO MỚI LỆNH XỬ LÝ" },
            { ApplicationPage.ClassifyScan, "HỒ SƠ KHÁCH HÀNG CÁ NHÂN" },
            { ApplicationPage.PreviewScan, "HỒ SƠ KHÁCH HÀNG CÁ NHÂN" },
            { ApplicationPage.SignaturePreview, "HỒ SƠ KHÁCH HÀNG CÁ NHÂN" },
            { ApplicationPage.ScanNoLogin, "SCAN NHANH" },
            { ApplicationPage.CaptureList, "OCR TRACKING" },
            { ApplicationPage.QuickScanLogin, "SCAN NHANH" },
            { ApplicationPage.VHSScan, "HỢP ĐỒNG CHUYỂN VẬN HÀNH SỐ" },
            { ApplicationPage.AutoUpload, "AUTO UPLOAD" },
            { ApplicationPage.AutoUploadV2, "ĐẶT LỊCH AUTO UPLOAD" },
            { ApplicationPage.TSDBScanImport, "HOÀN THIỆN TÀI SẢN ĐẢM BẢO" },
            { ApplicationPage.TSDBPreview, "HOÀN THIỆN TÀI SẢN ĐẢM BẢO" },
            { ApplicationPage.KHDNClassifyScan, "HỒ SƠ KHÁCH HÀNG DOANH NGHIỆP" },
            { ApplicationPage.KHDNPreviewScan, "HỒ SƠ KHÁCH HÀNG DOANH NGHIỆP" },
            { ApplicationPage.KHDNSignaturePreview, "HỒ SƠ KHÁCH HÀNG DOANH NGHIỆP" },
            { ApplicationPage.CCTClassifyScan, "CHẤM CHỨNG TỪ CUỐI NGÀY" },
            { ApplicationPage.CCTPreviewScan, "CHẤM CHỨNG TỪ CUỐI NGÀY" },
            { ApplicationPage.HSTHEClassifyScan, "HỒ SƠ THẺ" },
            { ApplicationPage.HSTHEPreviewScan, "HỒ SƠ THẺ" },
            { ApplicationPage.TDCNClassifyScan, "HỒ SƠ TÍN DỤNG KHCN" },
            { ApplicationPage.TDCNPreviewScan, "HỒ SƠ TÍN DỤNG KHCN" },
            { ApplicationPage.TDDNClassifyScan, "HỒ SƠ TÍN DỤNG KHDN" },
            { ApplicationPage.TDDNPreviewScan, "HỒ SƠ TÍN DỤNG KHDN" },
            { ApplicationPage.TSDBDNScanImport, "HOÀN THIỆN TÀI SẢN ĐẢM BẢO" },
            { ApplicationPage.TSDBDNPreview, "HOÀN THIỆN TÀI SẢN ĐẢM BẢO" }
        };

        public static readonly Dictionary<BusinessTypeApp, List<ContractType>> ContractTypes = new Dictionary<BusinessTypeApp, List<ContractType>>()
        {
            {
                BusinessTypeApp.HSKH, new List<ContractType>()
                {
                    new ContractType { Name = "11007 - Hồ sơ khác", Value = "HOSOKHAC" },
                    new ContractType { Name = "11001 - Hợp đồng mở tài khoản", Value = "MOTK" },
                    new ContractType { Name = "11004 - Thay đổi thông tin", Value = "THAYDOITT" },
                    new ContractType { Name = "11002 - Bổ sung dịch vụ", Value = "BOSUNGDV" },
                    new ContractType { Name = "11005 - Đóng tài khoản", Value = "DONGTK" }
                }
            },

            {
                BusinessTypeApp.TSDB, new List<ContractType>()
                {
                    new ContractType { Name = "-- Chọn loại tài sản --", Value = "NONE" },
                    new ContractType { Name = "Bất động sản (VH)-200,100", Value = "27" },
                    new ContractType { Name = "QTS HTTTL-420", Value="28" },
                    new ContractType { Name = "PTVT (VH)-300", Value="29" },
                    new ContractType { Name = "Chứng khoán (VH)-700,750", Value="30" },
                    new ContractType { Name = "GTCG (VH)-530,600,900,800", Value="31" },
                    new ContractType { Name = "QTS đã có hiện tại-410", Value="32" },
                    new ContractType { Name = "Dây chuyền, MMTB (VH)-210", Value="33" },
                    new ContractType { Name = "TS khác (VH)-400,540,550,520,500,510", Value="34" }
                }
            }
        };

        public static readonly Dictionary<BusinessTypeApp, List<DocumentType>> DocumentTypes = new Dictionary<BusinessTypeApp, List<DocumentType>>()
        {
            {
                BusinessTypeApp.TSDB, new List<DocumentType>()
                {
                    new DocumentType { Name = "BIENBANBANGIAO", Value = "BIENBANBANGIAO" },
                    new DocumentType { Name = "BIENBANDINHGIA", Value="BIENBANDINHGIA" },
                    new DocumentType { Name = "CHUNGTUTHANHTOAN", Value="CHUNGTUTHANHTOAN" },
                    new DocumentType { Name = "DENGHIPHONGTOA", Value="DENGHIPHONGTOA" },
                    new DocumentType { Name = "DKGDBD", Value="DKGDBD" },
                    new DocumentType { Name = "GIAYTOSOHUU", Value="GIAYTOSOHUU" },
                    new DocumentType { Name = "HOPDONGTHECHAP", Value="HOPDONGTHECHAP" },
                    new DocumentType { Name = "HOSOBAOHIEM", Value="HOSOBAOHIEM" },
                    new DocumentType { Name = "HOSOKHAC", Value="HOSOKHAC" },
                    new DocumentType { Name = "XACNHANPHONGTOA", Value="XACNHANPHONGTOA" },
                }
            },
            {
                BusinessTypeApp.KHDN, new List<DocumentType>()
                {
                    new DocumentType { Name = "10001 - Giấy phép thành lập", Value = "10001" },
                    new DocumentType { Name = "10002 - Điều lệ", Value="10002" },
                    new DocumentType { Name = "10003 - Quyết định bổ nhiệm/VB ủy quyền", Value="10003" },
                    new DocumentType { Name = "10004 - Giấy tờ tùy thân", Value="10004" },
                    new DocumentType { Name = "12001 - Đề nghị mở TK", Value="12001" },
                    new DocumentType { Name = "12002 - Đề nghị bổ sung dịch vụ", Value="12002" },
                    new DocumentType { Name = "12003 - Hồ sơ mở TK theo mục đích riêng", Value="12003" },
                    new DocumentType { Name = "12004 - Đề nghị thay đổi thông tin", Value="12004" },
                    new DocumentType { Name = "12005 - Đề nghị đóng TK", Value="12005" },
                    new DocumentType { Name = "12006 - Hồ sơ đóng TK", Value="12006" },
                    new DocumentType { Name = "12007 - Hồ sơ khác", Value="12007" },
                }
            }
        };

        public static readonly Dictionary<BusinessTypeApp, List<SignatureType>> SignatureTypes = new Dictionary<BusinessTypeApp, List<SignatureType>>()
        {
            {
                BusinessTypeApp.KHDN, new List<SignatureType>()
                {
                    new SignatureType { Name = "Mẫu dấu", Value = "nguoi_dai_dien_hp_mark" },
                    new SignatureType { Name = "Người giao dịch", Value="nguoi_giao_dich_sign" },
                    new SignatureType { Name = "Kế toán trưởng", Value="ketoan_truong_sign" },
                    new SignatureType { Name = "UQ Kế toán trưởng", Value="ketoan_uyquyen_sign" },
                    new SignatureType { Name = "UQ chủ tài khoản", Value="nguoi_uy_quyen_tk_sign" },
                    new SignatureType { Name = "Chủ tài khoản", Value="nguoi_dai_dien_hp_sign" },
                    new SignatureType { Name = "Chữ ký khác", Value="chu_ky_khac" },
                }
            }
        };
#pragma warning restore S2386 // Mutable fields should not be "public static"
    }
}
