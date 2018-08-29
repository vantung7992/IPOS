function CSO_SO(b_chu, b_so_tp) {
    try {
        b_chu = CH_CSO(b_chu, b_so_tp);
        if (b_chu == "") return 0;
        while (b_chu.indexOf(',') >= 0) b_chu = b_chu.replace(',', '');
        return b_chu * 1.0;
    }
    catch (ex) { return 0; }
}
function SO_CSO(b_so, b_so_tp) {
    if (b_so == null) return "0";
    return CH_CSO(b_so.toString(), b_so_tp)
}
function CH_CNG(b_chu) {
    try {
        if (b_chu == "") return "";
        var b_moi = "", b_s, i;
        for (i = 0; i < b_chu.length; i++) {
            b_s = b_chu.substr(i, 1);
            if ("0123456789".indexOf(b_s) >= 0) b_moi = b_moi + b_s;
        }
        if (b_moi == "") return "";
        b_moi = b_moi + "          ";
        b_moi = b_moi.substr(0, 2) + "/" + b_moi.substr(2, 2) + "/" + b_moi.substr(4, 4);
        return b_moi;
    }
    catch (ex) { return b_chu; }
}
function CNG_CSO(b_chu) {
    if (Fb_NGAY_TRANG(b_chu)) b_chu = "01/01/3000";
    var b_loi = Fs_NGAY_LOI(b_chu, "K");
    if (b_loi != "") return "0";
    else return b_chu.substr(6, 4) + b_chu.substr(3, 2) + b_chu.substr(0, 2);
}
function CNG_SO(b_chu) {
    var b_moi = CNG_CSO(b_chu);
    return b_moi * 1.0;
}
function CSO_CNG(b_chu) {
    try {
        return b_chu.substr(6, 2) + "/" + b_chu.substr(4, 2) + "/" + b_chu.substr(0, 4);
    }
    catch (ex) { return "01/01/3000"; }
}
function SO_CNG(b_so) {
    return CSO_CNG(b_so.toString())
}
// Chuyen chu dang dd/mm/yyyy thang dang date
function CNG_NG(b_ngay) {
    if (Fb_NGAY_TRANG(b_ngay)) b_ngay = "01/01/3000";
    var b_d = parseInt(b_ngay.substr(0, 2), 10), b_m = parseInt(b_ngay.substr(3, 2) - 1, 10), b_y = parseInt(b_ngay.substr(6, 4), 10);
    var b_kq = new Date(b_y, b_m, b_d);
    return b_kq;
}
// Chuyen date sang chu dang dd/mm/yyyy
function NG_CNG(b_ngay) {
    var b_ng = b_ngay.getDate().toString(), b_th = (b_ngay.getMonth() + 1).toString();
    if (b_ng.length < 2) b_ng = "0" + b_ng;
    if (b_th.length < 2) b_th = "0" + b_th;
    return b_ng + '/' + b_th + '/' + b_ngay.getFullYear().toString();
}
// So ngay trong thang
function Fi_NGAY_THANG(b_m, b_y) {
    var b_d = 31, b_m_c = b_m.toString();
    if (Fb_MA_MA("4,6,9,11", b_m_c)) b_d = 30;
    else if (b_m == 2) b_d = (ROUNDN(b_y / 4, 1) == ROUNDN(b_y / 4, 0)) ? 29 : 28;
    return b_d;
}
// Tang thang cho ngay
function Fs_NGAY_THANG(b_ngay, b_thang) {
    if (b_thang == 0) return b_ngay;
    var b_d = parseInt(b_ngay.substr(0, 2), 10), b_m = parseInt(b_ngay.substr(3, 2), 10), b_y = parseInt(b_ngay.substr(6, 4), 10);
    if (b_thang > 0) {
        b_m += b_thang;
        while (b_m > 12) { b_y++; b_m -= 12; }
    }
    else if (b_thang < 0) {
        b_m -= b_thang;
        while (b_m < 1) { b_y--; b_m += 12; }
    }
    return SO_CNG(b_d + b_m * 100 + b_y * 10000);
}
function Fi_SOTHANG(d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months = months + d2.getMonth() - d1.getMonth();
    if (d2.getDate() > d1.getDate()) months++;
    return months <= 0 ? 0 : months;
}
function P_NGAY_KTRA(strValue) {
    var reDay = /^([0-9]){2}(\/){1}([0-9]){2}(\/)([0-9]){4}$/;
    if (strValue.search(reDay) == -1) //if match failed
    {
        alert("Bạn phải nhập đúng định dạng DD/MM/YYYY. Ví dụ: 23/01/2010");
        return false;
    }
    else {
        if ((parseInt(strValue.substring(0, 2), 10) < 0) || (parseInt(strValue.substring(0, 2), 10) > 31)) {
            alert("Giá trị ngày phải nằm trong giới hạn 1->31");
            return false;
        }
        if ((parseInt(strValue.substring(3, 5), 10) < 0) || (parseInt(strValue.substring(3, 5), 10) > 12)) {
            alert("Giá trị tháng phải nằm trong giới hạn 1->12");
            return false;
        }
        if ((parseInt(strValue.substring(6, 10), 10) < 1900) || (parseInt(strValue.substring(6, 10), 10) > 2100)) {
            alert("Giá trị năm phải nằm trong giới hạn 1900->2100");
            return false;
        }
    }
    return true;
}
//Loc ky tu trang
function C_NVL(b_in) { // Dan
    var b_kq = (b_in == null) ? "" : b_in.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
    if (b_kq.toUpperCase() == "NULL") b_kq = "";
    return b_kq;
}
//Loc ky ky tu trang. Neu b_in ="" tra b_out
function O_NVL(b_in, b_out) { // Dan
    b_in = C_NVL(b_in);
    return (b_in == "") ? b_out : b_in;
}
//Lọc ký tự trắng bên phải
function R_NVL(b_in) { // Dan
    return (b_in == null) ? "" : b_in.replace(/\s+$/, "");
}
//Ngay - Kiem tra ngay trang
function Fb_NGAY_TRANG(b_ngay) {
    if (b_ngay == null) return true;
    b_ngay = C_NVL(b_ngay);
    while (b_ngay.indexOf('/') >= 0) b_ngay = b_ngay.replace('/', '');
    return (b_ngay == "");
}
function Fb_MANG(b_obj) {
    if (b_obj instanceof Array) return true;
    else return false;
}
function CH_CSO(b_chu, b_so_tp) {
    try {
        if (b_chu == null || b_chu == "") return "";
        var b_moi = "", b_chan = "", b_tp = "", b_dau = "", b_cham = "", b_s, i;
        for (i = 0; i < b_chu.length; i++) {
            b_s = b_chu.substr(i, 1);
            if (b_s == ".") b_cham = ".";
            else if (b_s == "-") b_dau = "-";
            else if ("0123456789".indexOf(b_s) >= 0) {
                if (b_cham == ".") b_tp = b_tp + b_s;
                else if (b_chan == "0") b_chan = b_s;
                else b_chan = b_chan + b_s;
            }
        }
        if (b_chan.length > 3) {
            while (b_chan != "") {
                i = b_chan.length - 3;
                if (i < 0) i = 0;
                if (b_moi != "") b_moi = b_chan.substr(i) + "," + b_moi;
                else b_moi = b_chan.substr(i);
                if (i == 0) b_chan = ""; else b_chan = b_chan.substr(0, i);
            }
        }
        else b_moi = b_chan;
        b_moi = b_dau + b_moi;
        if (b_so_tp != null) {
            if (b_so_tp > 0 && b_tp.length > b_so_tp) b_tp = b_tp.substr(0, b_so_tp);
            b_moi = b_moi + b_cham + b_tp;
        }
        return b_moi;
    }
    catch (ex) { return b_chu; }
}
//Ngay - Kiem tra nhap
function Fs_NGAY_LOI(b_ngay, b_kieu) {
    if (Fb_NGAY_TRANG(b_ngay)) return "";
    var b_kdang = "##/##/####";
    if (b_ngay.length != b_kdang.length) return "Sai kiểu ngày";
    for (var i = 0; i < b_kdang.length; i++) {
        var b_c = b_ngay.substr(i, 1);
        if (b_kdang.substr(i, 1) != "#") {
            if (b_c != b_kdang.substr(i, 1)) return "Sai kiểu ngày";
        }
        else if ("0123456789".indexOf(b_c) == -1) return "Sai kiểu ngày";
    }
    var b_d = parseInt(b_ngay.substr(0, 2), 10), b_m = parseInt(b_ngay.substr(3, 2), 10), b_y = parseInt(b_ngay.substr(6, 4), 10);
    if (b_m < 1 || b_m > 12) return "Tháng từ 1-12";
    if (b_y < 1900 || b_y > 3000) return "Năm từ 1900-3000";
    if (b_kieu == "C" && (b_d < 1 || b_d > Fi_NGAY_THANG(b_m, b_y))) return "Sai kiểu ngày";
    return "";
}
function ROUNDN(b_so, b_tp) {
    return Math.round(b_so * Math.pow(10, b_tp)) / Math.pow(10, b_tp);
}
//Tim chuoi trong chuoi dang dung
function Fb_MA_MA(b_ngoai, b_trong) { // Dan
    if (b_ngoai == null || b_ngoai == "" || b_trong == null || b_trong == "") return false;
    b_ngoai = b_ngoai.toUpperCase(); b_trong = b_trong.toUpperCase();
    var a_ngoai = b_ngoai.split(',');
    for (var i = 0; i < a_ngoai.length; i++)
        if (a_ngoai[i] == b_trong) return true;
    return false;
}
//Tim chuoi trong chuoi dang dung dau
function Fb_MA_MA_D(b_ngoai, b_trong) { // Dan
    if (b_ngoai == null || b_ngoai == "" || b_trong == null || b_trong == "") return false;
    b_ngoai = b_ngoai.toUpperCase(); b_trong = b_trong.toUpperCase();
    var a_ngoai = b_ngoai.split(',');
    for (var i = 0; i < a_ngoai.length; i++)
        if (b_trong.indexOf(a_ngoai[i]) == 0) return true;
    return false;
}
//Tim chuoi trong chuoi dang gan dung
function Fb_MA_MA_G(b_ngoai, b_trong) { // Dan
    if (b_ngoai == null || b_ngoai == "" || b_trong == null || b_trong == "") return false;
    b_ngoai = b_ngoai.toUpperCase(); b_trong = b_trong.toUpperCase();
    var a_ngoai = b_ngoai.split(',');
    for (var i = 0; i < a_ngoai.length; i++)
        if (b_trong.indexOf(a_ngoai[i]) >= 0) return true;
    return false;
}
function P_RADIO_CHON(b_id, b_gtri) {
    var a_ctr = $("#" + b_id + " :radio");
    for (var i = 0; i < a_ctr.length; i++) {
        if (a_ctr[i].value == b_gtri) a_ctr[i].checked = true;
        else a_ctr[i].checked = false;
    }
}
// Mo form
var form_choId = 0, form_choTen = "", form_choTso, form_choDem = 0;
function form_P_MO(b_tenf, b_mo, a_tso, b_bbuoc) {
    try {
        var b_goc = form_F_GOC(), b_ten = form_Fs_TEN(b_tenf);
        var b_damo = b_goc.damo;
        if (b_damo == null || b_damo == undefined) b_damo = "";
        if (eval("b_goc." + b_ten) == null || b_bbuoc == "C") {
            eval("b_goc." + b_ten + "=b_goc.open('" + b_tenf + "', '', 'resizable');");
            if (!Fb_MA_MA(b_damo, b_ten)) b_goc.damo = kytu_Fs_them(b_damo, b_ten);
        }
        eval("b_goc." + b_ten + ".damo=b_mo;"); eval("b_goc." + b_ten + ".focus();");
        if (a_tso != null && a_tso != "") {
            if (eval("b_goc." + b_ten + ".P_KET_QUA") != null) eval("b_goc." + b_ten + ".P_KET_QUA(a_tso[0], a_tso[1]);");
            else {
                if (form_choId != 0) clearTimeout(form_choId);
                form_choTen = b_ten; form_choTso = a_tso; form_choDem = 0;
                form_choId = setInterval('form_P_MO_CHO()', 500);
            }
        }
    }
    catch (err) { }
}
function form_P_MO_CHO() {
    form_choDem++;
    if (form_choDem > 5) clearTimeout(form_choId);
    var b_goc = form_F_GOC();
    if (eval("b_goc." + form_choTen + ".P_KET_QUA") != null) {
        clearTimeout(form_choId);
        eval("b_goc." + form_choTen + ".P_KET_QUA(form_choTso[0], form_choTso[1]);");
    }
}
// Dong form
function form_P_DONG(b_ten, a_tso) {
    try {
        if (b_ten == "login") return;
        var b_goc = form_F_GOC();
        var b_damo = b_goc.damo;
        if (b_damo == null || b_damo == undefined || b_damo == "") return;
        var a_damo = b_damo.split(',');
        if (b_ten != "menu") {
            var b_tra = eval("b_goc." + b_ten + ".damo;");
            eval("b_goc." + b_ten + ".close();"); eval("b_goc." + b_ten + "=null;");
            b_damo = "";
            for (var i = 0; i < a_damo.length; i++) {
                if (a_damo[i] != b_ten) b_damo = kytu_Fs_them(b_damo, a_damo[i]);
            }
            b_goc.damo = b_damo;
            if (a_tso != null && b_tra != null && b_tra != "") {
                var a_s = b_tra.split(',');
                if (Fb_MA_MA(b_damo, a_s[0])) {
                    if (eval("b_goc." + a_s[0] + ".P_KET_QUA") != null) {
                        eval("b_goc." + a_s[0] + ".focus();");
                        eval("b_goc." + a_s[0] + ".P_KET_QUA(a_s[1], a_tso);");
                    }
                }
            }
        }
        else {
            for (var i = 0; i < a_damo.length; i++) {
                eval("b_goc." + a_damo[i] + ".close();"); eval("b_goc." + a_damo[i] + "=null;");
            }
        }
    }
    catch (err) { }
}
// Tra Form goc
function form_F_GOC() {
    var b_g = window, b_f = window.opener;
    try {
        while (b_f != null && b_f != undefined && b_f.name != null) { b_g = b_f; b_f = b_g.opener; }
        b_f = null;
    }
    catch (err) { }
    return b_g;
}
function form_P_MOI(b_vungId, b_dk) {
    try {
        var a_vungId = b_vungId.split(',');var b_kt_xoa;
        for (var j = 0; j < a_vungId.length; j++) {
            var b_vung = $get(form_Fs_ClientID(a_vungId[j]));
            var a_ctr = b_vung.getElementsByTagName("input");
            for (var i = 0; i < a_ctr.length; i++) {
                b_kt_xoa = a_ctr[i].kt_xoa;
                if (b_kt_xoa == null && a_ctr[i].attributes['kt_xoa'] != null && a_ctr[i].attributes['kt_xoa'].value != null) b_kt_xoa = a_ctr[i].attributes['kt_xoa'].value;
                if (b_kt_xoa != null && b_kt_xoa != undefined) {
                    if (b_dk.indexOf(b_kt_xoa) >= 0) {
                        if (a_ctr[i].type == "text") a_ctr[i].value = "";
                    }
                }
            }
            var a_ctr = b_vung.getElementsByTagName("textarea");
            for (var i = 0; i < a_ctr.length; i++) {
                b_kt_xoa = a_ctr[i].kt_xoa;
                if (b_kt_xoa == null && a_ctr[i].attributes['kt_xoa'] != null && a_ctr[i].attributes['kt_xoa'].value != null) b_kt_xoa = a_ctr[i].attributes['kt_xoa'].value;
                if (b_kt_xoa != null && b_kt_xoa != undefined) {
                    if (b_dk.indexOf(b_kt_xoa) >= 0) a_ctr[i].value = "";
                }
            }
            a_ctr = b_vung.getElementsByTagName("span");
            for (var i = 0; i < a_ctr.length; i++) {
                b_kt_xoa = a_ctr[i].kt_xoa;
                if (b_kt_xoa == null && a_ctr[i].attributes['kt_xoa'] != null && a_ctr[i].attributes['kt_xoa'].value != null) b_kt_xoa = a_ctr[i].attributes['kt_xoa'].value;
                if (b_kt_xoa != null && b_kt_xoa != undefined) {
                    if (b_dk.indexOf(b_kt_xoa) >= 0) a_ctr[i].innerHTML = "";
                }
            }
        }
    }
    catch (err) { }
}
function form_Fs_TEXT_KTRA(b_vungId) {
    try {
        var b_gtri = "", b_ten = "", b_loi = [""], b_kt = -1, a_ten = [], a_gtri = [], b_nhap = true;
        var a_vungId = b_vungId.split(',');
        for (var j = 0; j < a_vungId.length; j++) {
            var b_vung = $get(form_Fs_ClientID(a_vungId[j]));
            var a_ctr = b_vung.getElementsByTagName("input");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_nhap = (a_ctr[i].id == a_ctr[i].id.toUpperCase());
                    if (b_nhap) {
                        b_gtri = C_NVL(a_ctr[i].value); b_ten = C_NVL(obj_Fs_Attribute(a_ctr[i], 'ten'));
                        if (!b_ten) b_ten = a_ctr[i].id;
                        if (obj_Fs_Attribute(a_ctr[i],'kieu_date') != null) {
                            if (Fb_NGAY_TRANG(b_gtri)) return "Phải nhập " + b_ten;
                            b_loi = Fs_NGAY_LOI(b_gtri, obj_Fs_Attribute(a_ctr[i],'kieu_date'));
                            if (b_loi != "") return b_loi + b_ten;
                        }
                        else if (obj_Fs_Attribute(a_ctr[i],'so_tp') != null) {
                            if (CSO_SO(b_gtri, 0) == 0) return "Phải nhập " + b_ten;
                        }
                        else if (b_gtri == "") return "Phải nhập " + b_ten;
                    }
                }
            }
            a_ctr = b_vung.getElementsByTagName("span");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    if (a_ctr[i].id == a_ctr[i].id.toUpperCase() && C_NVL(a_ctr[i].innerHTML) == "")
                        return "Phải nhập " + C_NVL(obj_Fs_Attribute(a_ctr[i],'ten'));
                }
            }
        }
        return "";
    }
    catch (err) {return (err.message ? err.message : err); }
}
function form_Faa_TEXT_ROW(b_vungId) {
    try {
        var b_gtri = "", b_kt = -1, a_ten = [], a_gtri = [];
        var a_vungId = b_vungId.split(',');
        for (var j = 0; j < a_vungId.length; j++) {
            var b_vung = $get(form_Fs_ClientID(a_vungId[j]));
            var a_ctr = b_vung.getElementsByTagName("input");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i],'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    if (obj_Fs_Attribute(a_ctr[i], 'kieu_date') != null) {
                        if (obj_Fs_Attribute(a_ctr[i], 'kieu_luu') == "D") a_gtri[b_kt] = CNG_NG(b_gtri);
                        else if (obj_Fs_Attribute(a_ctr[i], 'kieu_luu') == "I") a_gtri[b_kt] = CNG_SO(b_gtri);
                        else a_gtri[b_kt] = b_gtri;
                    }
                    else if (obj_Fs_Attribute(a_ctr[i], 'so_tp') != null) a_gtri[b_kt] = CSO_SO(b_gtri, 0);
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_chu') != null) a_gtri[b_kt] = b_gtri.toUpperCase();
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_unicode') == "C" && b_gtri != "") a_gtri[b_kt] = "N'" + b_gtri;
                    else a_gtri[b_kt] = b_gtri;
                }
            }
            var a_ctr = b_vung.getElementsByTagName("table");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C" && obj_Fs_Attribute(a_ctr[i], 'chon_radio')=="C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    var a_con = a_ctr[i].getElementsByTagName("input");
                    for (var k = 0; k < a_con.length; k++) {
                        if (a_con[k].checked) b_gtri = C_NVL(a_con[k].value);
                    }
                    a_gtri[b_kt] = b_gtri;
                }
            }
            a_ctr = b_vung.getElementsByTagName("textarea");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i],'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    if (obj_Fs_Attribute(a_ctr[i], 'kieu_unicode') == "C" && b_gtri != "") a_gtri[b_kt] = "N'" + b_gtri;
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_chu') != null) a_gtri[b_kt] = b_gtri.toUpperCase();
                    else a_gtri[b_kt] = b_gtri;
                }
            }
            a_ctr = b_vung.getElementsByTagName("span");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].ten_goc; a_gtri[b_kt] = C_NVL(a_ctr[i].innerHTML);
                }
            }
            a_ctr = b_vung.getElementsByTagName("select");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    if (obj_Fs_Attribute(a_ctr[i],'kieu_unicode') == "C") a_gtri[b_kt] = "N'" + b_gtri;
                    else a_gtri[b_kt] = b_gtri;
                }
            }
        }
        return [a_ten, a_gtri];
    }
    catch (err) { form_P_LOI(err); }
}
function form_Faa_TEXT_ROW_2(b_vungId) {
    try {
        var b_gtri = "", b_kt = -1, a_ten = [], a_gtri = [], a_gtri_u = [], a_gtri_n = [], a_gtri_d = [];
        var a_vungId = b_vungId.split(',');
        for (var j = 0; j < a_vungId.length; j++) {
            var b_vung = $get(form_Fs_ClientID(a_vungId[j]));
            var a_ctr = b_vung.getElementsByTagName("input");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    if (obj_Fs_Attribute(a_ctr[i], 'kieu_date') != null) {
                        if (obj_Fs_Attribute(a_ctr[i], 'kieu_luu') == "D") a_gtri_d[b_kt] = CNG_NG(b_gtri);
                        else if (obj_Fs_Attribute(a_ctr[i], 'kieu_luu') == "I") a_gtri_n[b_kt] = CNG_SO(b_gtri);
                        else a_gtri[b_kt] = b_gtri;
                    }
                    else if (obj_Fs_Attribute(a_ctr[i], 'so_tp') != null) a_gtri_n[b_kt] = CSO_SO(b_gtri, 0);
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_so') != null) a_gtri_n[b_kt] = CSO_SO(b_gtri, 0);
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_chu') != null) a_gtri[b_kt] = b_gtri.toUpperCase();
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_unicode') == "C" && b_gtri != "") a_gtri_u[b_kt] = b_gtri;
                    else a_gtri[b_kt] = b_gtri;
                }
            }
            var a_ctr = b_vung.getElementsByTagName("table");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C" && obj_Fs_Attribute(a_ctr[i], 'chon_radio') == "C") {
                    var a_con = a_ctr[i].getElementsByTagName("input");
                    for (var k = 0; k < a_con.length; k++) {
                        if (a_con[k].checked) {
                            b_kt++; a_ten[b_kt] = a_ctr[i].id; a_gtri[b_kt] = C_NVL(a_con[k].value);
                        }
                    }
                }
            }
            a_ctr = b_vung.getElementsByTagName("textarea");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    if (obj_Fs_Attribute(a_ctr[i], 'kieu_unicode') == "C" && b_gtri != "") a_gtri_u[b_kt] = b_gtri;
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_chu') != null) a_gtri[b_kt] = b_gtri.toUpperCase();
                    else a_gtri[b_kt] = b_gtri;
                }
            }
            a_ctr = b_vung.getElementsByTagName("span");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].ten_goc; a_gtri[b_kt] = C_NVL(a_ctr[i].innerHTML);
                }
            }
            a_ctr = b_vung.getElementsByTagName("select");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C") {
                    b_kt++; a_ten[b_kt] = a_ctr[i].id; b_gtri = C_NVL(a_ctr[i].value);
                    if (obj_Fs_Attribute(a_ctr[i], 'kieu_unicode') == "C") a_gtri_u[b_kt] = b_gtri;
                    else a_gtri[b_kt] = b_gtri;
                }
            }
        }
        if (!a_gtri[b_kt]) a_gtri[b_kt] = null;
        if (!a_gtri_u[b_kt]) a_gtri_u[b_kt] = null;
        if (!a_gtri_n[b_kt]) a_gtri_n[b_kt] = null;
        return [a_ten, a_gtri, a_gtri_u, a_gtri_n, a_gtri_d];
    }
    catch (err) { form_P_LOI(err); }
}
function form_P_MANG_TEXT(b_vungId, a_ten, a_gtri) {
    try {
        var b_vtri = 0, b_ten = "", a_vungId = b_vungId.split(',');
        for (var j = 0; j < a_vungId.length; j++) {
            var b_vung = $get(form_Fs_ClientID(a_vungId[j]));
            var a_ctr = b_vung.getElementsByTagName("input");
            for (var i = 0; i < a_ctr.length; i++) {
                b_ten = a_ctr[i].id.toUpperCase(); b_vtri = jQuery.inArray(b_ten, a_ten);
                if (b_vtri >= 0) {
                    if (obj_Fs_Attribute(a_ctr[i], 'so_tp') != null) a_ctr[i].value = SO_CSO(a_gtri[b_vtri], 0);
                    else if (obj_Fs_Attribute(a_ctr[i], 'kieu_luu') == "I") a_ctr[i].value = CSO_CNG(a_gtri[b_vtri]);
                    else a_ctr[i].value = C_NVL(a_gtri[b_vtri]);
                }
            }
            a_ctr = b_vung.getElementsByTagName("textarea");
            for (var i = 0; i < a_ctr.length; i++) {
                b_ten = a_ctr[i].id.toUpperCase(); b_vtri = jQuery.inArray(b_ten, a_ten);
                if (b_vtri >= 0) a_ctr[i].value = C_NVL(a_gtri[b_vtri]);
            }
            a_ctr = b_vung.getElementsByTagName("span");
            for (var i = 0; i < a_ctr.length; i++) {
                b_ten = a_ctr[i].id.toUpperCase(); b_vtri = jQuery.inArray(b_ten, a_ten);
                if (b_vtri >= 0)
                    if (a_ctr[i].text) a_ctr[i].text(a_gtri[b_vtri]); else a_ctr[i].innerText = a_gtri[b_vtri];
            }
            a_ctr = b_vung.getElementsByTagName("select");
            for (var i = 0; i < a_ctr.length; i++) {
                b_ten = a_ctr[i].id.toUpperCase(); b_vtri = jQuery.inArray(b_ten, a_ten);
                if (b_vtri >= 0) a_ctr[i].value = a_gtri[b_vtri];
            }
            a_ctr = b_vung.getElementsByTagName("radio");
            for (var i = 0; i < a_ctr.length; i++) {
                b_ten = a_ctr[i].id.toUpperCase();
                var b_i = b_ten.lastIndexOf('_');
                if (b_i == -1) b_i = b_ten.length;
                b_ten = "".substr(0, b_i - 1);
                b_vtri = jQuery.inArray(b_ten, a_ten);
                if (b_vtri >= 0 && a_ctr[i].value == a_gtri[b_vtri]) a_ctr[i].selected = true;
            }
            var a_ctr = b_vung.getElementsByTagName("table");
            for (var i = 0; i < a_ctr.length; i++) {
                if (obj_Fs_Attribute(a_ctr[i], 'nhap') == "C" && obj_Fs_Attribute(a_ctr[i], 'chon_radio') == "C") {
                    b_ten = a_ctr[i].id.toUpperCase();
                    b_vtri = jQuery.inArray(b_ten, a_ten);
                    var a_con = a_ctr[i].getElementsByTagName("input");
                    for (var k = 0; k < a_con.length; k++) {
                        if (a_con[k].value == a_gtri[b_vtri]) a_con[k].checked = true;
                    }
                }
            }
        }
    }
    catch (err) { form_P_LOI(err); }
}
function form_P_CH_TEXT(b_vungId, b_ch) {
    try {
        if (b_ch == "") return;
        var a_kq = b_ch.split(';');
        var a_ten = a_kq[0].split(','), a_gtri = a_kq[1].split('|');
        form_P_MANG_TEXT(b_vungId, a_ten, a_gtri);
    }
    catch (err) { form_P_LOI(err); }
}
//jqgrid
function P_JQGRID_CB(b_data, grid) {
    var a_table = b_data.split('#')
    var a_cols = a_table[0].split(','), a_rows = a_table[1].split(';'), a_kieu = a_table[2].split(''), a_colModel = [];
    for (var i = 0; i < a_cols.length; i++) {
        a_colModel[i] = { name: a_cols[i], index: a_cols[i] };
        if (a_kieu[i] == 'N') {
            a_colModel[i].formatter = "integer"; a_colModel[i].align = "right";
        }
    }
    grid.jqGrid({
        datatype: 'local',
        colNames: a_cols,
        colModel: a_colModel,
        gridview: true,
        autowidth: true,
        shrinkToFit: true,
        viewrecords: true,
        rownumbers: true
    });
    for (var i = 0; i < a_rows.length; i++) {
        a_val = a_rows[i].split('|');
        b_row = {}
        for (var j = 0; j < a_val.length; j++) {
            b_row[a_cols[j]] = a_val[j];
        }
        grid.jqGrid('addRowData', i, b_row);
    }
}
 //Hiện lỗi
function form_P_LOI(b_loi) {
    try {
        if (b_loi == null || b_loi == "") return;
        if (b_loi.message) return; // b_loi = b_loi.message;
        var b_i = b_loi.indexOf("loi:");
        if (b_i >= 0) b_loi = b_loi.substr(b_i + 4);
        b_i = b_loi.indexOf(":loi");
        if (b_i > 0) b_loi = b_loi.substr(0, b_i);
        if (b_loi != "") P_alert(b_loi);
    }
    catch (err) { }
}
function form_Fs_ClientID(b_ctr) {
    return b_ctr;
}
function form_P_Enter2Tab() {
    $('form').keydown(function (e) {
        var b_code = e.keyCode ? e.keyCode : e.which;
        if (b_code != 13) return true;
        if (e.target.type == 'text' || e.target.type == 'radio' || e.target.type.indexOf('select') > -1) {
            var fields = $(this).find('input,select,button');
            var index = -1;
            for (var i = 0; i < fields.length; i++) {
                if (e.target.type != "radio" || fields.eq(i)[0].type != "radio")
                    if (index > -1 && !fields.eq(i)[0].disabled) { index = i; break; }
                if (fields.eq(i)[0] == e.target) index = i;
            }
            if (!b_event_cancel) {
                if (index < fields.length) {
                    fields.eq(index).select();
                    fields.eq(index).focus();
                }
                else {
                    fields.eq(0).select();
                    fields.eq(0).focus();
                }
            }
            else b_event_cancel = false;
            if (e.stopPropagation) e.stopPropagation();
            if (e.preventDefault) e.preventDefault();
            return false;
        }
    });
}
// Tra thu tu trong mang a_ten theo b_ten
function Fi_vtri_mang(a_ten, b_ten) {
    var b_vtri = -1;
    for (var i = 0; i < a_ten.length; i++) {
        if (a_ten[i].toUpperCase() == b_ten.toUpperCase()) { b_vtri = i; break; }
    }
    return b_vtri;
}
// Tra gia tri trong mang a_gtri theo b_ten
function Fobj_gtri_mang(a_ten, a_gtri, b_ten) {
    var b_vtri = Fi_vtri_mang(a_ten, b_ten);
    return (b_vtri < 0) ? null : a_gtri[b_vtri];
}
// Tra mang gia tri trong mang a_gtri theo mang a_tim
function Faobj_gtri_mang(a_ten, a_gtri, a_tim) {
    var a_tra = [];
    for (var i = 0; i < a_tim.length; i++) a_tra[i] = Fobj_gtri_mang(a_ten, a_gtri, a_tim[i]);
    return a_tra;
}
// Chuyen gia tri trong mang tu ten nay sang ten khac
function Faobj_chuyen_mang(a_ten, a_gtri, b_ten1, b_ten2) {
    var b_i = 0, b_j = 0, b_c = "";
    b_i = Fi_vtri_mang(a_ten, b_ten1);
    b_j = Fi_vtri_mang(a_ten, b_ten2);
    b_c = a_gtri[b_i];
    a_gtri[b_i] = a_gtri[b_j];
    a_gtri[b_j] = b_c;
}
function P_xoa_dong_mang(a_ten, a_gtri, b_ten) {
    var b_i = Fi_vtri_mang(a_ten, b_ten);
    a_ten.splice(b_i, 1); a_gtri.splice(b_i, 1);
}
function Pas_tach(b_s, a_ten, a_gtri) {
    var a_row = b_s.split(';');
    var a_cell;
    for (var i = 0; i < a_row.length; i++) {
        a_cell = a_row[i].split('|');
        a_ten[i] = a_cell[0]; a_gtri[i] = a_cell[1];
    }
}
function form_P_MA_Inhoa() {
    $('input[class~="css_ma"]').keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });
}
function form_P_NGAY_Edit() {
    if ($('input[kieu_date="C"]').length > 0) {
        $('input[kieu_date="C"]').mask('99/99/9999', { type: 'Date' });
//        $('input[kieu_date="C"]').datepicker({
//            dateFormat: 'dd/mm/yy', showOn: "button", buttonText: ".."
//        });
    }
    if ($('input[kieu_time="C"]').length > 0) {
        $('input[kieu_time="C"]').mask('99:99');
    }
}
function form_P_Tooltip() {
    $(document).tooltip();
}
function form_OnLoad() {
    form_P_Enter2Tab();
    //form_P_MA_Inhoa();
    form_P_NGAY_Edit();
    //form_P_Tooltip();
}
function getParameterByName(name) {

    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);

    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));

}
function obj_Fget(b_ten) {
    return $get(form_Fs_ClientID(b_ten));
}
function obj_Fs_value(b_ten) {
    var b_ctr = $get(form_Fs_ClientID(b_ten));
    var b_gtri = C_NVL(b_ctr.value);

    if (obj_Fs_Attribute(b_ctr, 'nhap') == "C" && obj_Fs_Attribute(b_ctr, 'chon_radio') == "C") {
        var a_con = b_ctr.getElementsByTagName("input");
        for (var k = 0; k < a_con.length; k++) {
            if (a_con[k].checked) b_gtri += C_NVL(a_con[k].value);
        }
    }

    if (obj_Fs_Attribute(b_ctr, 'kieu_chu')) b_gtri = b_gtri.toUpperCase();
    return b_gtri;
}
function obj_Fs_Attribute(obj, attr) {
    if (!obj) return null;
    if (!obj.attributes) return null;
    if (!obj.attributes[attr]) return null;
    return obj.attributes[attr].value;
}
var b_event_cancel = false;
function ctr_Pfocus(b_ten) {
    $get(form_Fs_ClientID(b_ten)).focus();
    b_event_cancel = true;
}
function P_alert(b_tbao) {
    alert(b_tbao); 
}
function P_LOI_TGIAN(b_loi) { P_alert("Lỗi đường truyền"); }
function P_LOI_CSDL(b_loi) { P_alert(b_loi._message); }
function Fb_LOI_KTRA(b_loi) {
    if (C_NVL(b_loi) == "") return false;
    return (b_loi.indexOf("loi:") >= 0 && b_loi.lastIndexOf(":loi") >= 0);
}
function P_LOI_KQ(b_loi) {
    if (Fb_LOI_KTRA(b_loi)) form_P_LOI(b_loi);
}
function form_P_BD_Loading() {
    $("#main_loading").fadeIn(200);
}
function form_P_KT_Loading() {
    $("#main_loading").fadeOut(200);
}