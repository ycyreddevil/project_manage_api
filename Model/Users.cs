using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Users
    {
        /// <summary>
        /// 
        /// </summary>
        public Users()
        {
        }

        private int _userId;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int userId { get { return this._userId; } set { this._userId = value; } }

        private System.String _wechatUserId;
        /// <summary>
        /// 
        /// </summary>
        public System.String wechatUserId { get { return this._wechatUserId; } set { this._wechatUserId = value; } }

        private System.String _userName;
        /// <summary>
        /// 姓名
        /// </summary>
        public System.String userName { get { return this._userName; } set { this._userName = value; } }

        private System.String _mobilePhone;
        /// <summary>
        /// 手机号
        /// </summary>
        public System.String mobilePhone { get { return this._mobilePhone; } set { this._mobilePhone = value; } }

        private System.String _passWord;
        /// <summary>
        /// 密码
        /// </summary>
        public System.String passWord { get { return this._passWord; } set { this._passWord = value; } }

        private System.String _sex;
        /// <summary>
        /// 性别
        /// </summary>
        public System.String sex { get { return this._sex; } set { this._sex = value; } }

        private System.DateTime? _birthday;
        /// <summary>
        /// 生日
        /// </summary>
        public System.DateTime? birthday { get { return this._birthday; } set { this._birthday = value; } }

        private System.DateTime? _hiredate;
        /// <summary>
        /// 入职日期
        /// </summary>
        public System.DateTime? hiredate { get { return this._hiredate; } set { this._hiredate = value; } }

        private System.String _regularEmployeeDate;
        /// <summary>
        /// 转正日期
        /// </summary>
        public System.String regularEmployeeDate { get { return this._regularEmployeeDate; } set { this._regularEmployeeDate = value; } }

        private System.String _idNumber;
        /// <summary>
        /// 身份证号
        /// </summary>
        public System.String idNumber { get { return this._idNumber; } set { this._idNumber = value; } }

        private System.String _nativePlace;
        /// <summary>
        /// 出生地
        /// </summary>
        public System.String nativePlace { get { return this._nativePlace; } set { this._nativePlace = value; } }

        private System.String _employeeCode;
        /// <summary>
        /// 工号
        /// </summary>
        public System.String employeeCode { get { return this._employeeCode; } set { this._employeeCode = value; } }

        private System.String _address;
        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get { return this._address; } set { this._address = value; } }

        private System.String _graduationSchool;
        /// <summary>
        /// 毕业学校
        /// </summary>
        public System.String graduationSchool { get { return this._graduationSchool; } set { this._graduationSchool = value; } }

        private System.String _major;
        /// <summary>
        /// 专业
        /// </summary>
        public System.String major { get { return this._major; } set { this._major = value; } }

        private System.String _education;
        /// <summary>
        /// 学历
        /// </summary>
        public System.String education { get { return this._education; } set { this._education = value; } }

        private System.Int32 _companyId;
        /// <summary>
        /// 公司Id
        /// </summary>
        public System.Int32 companyId { get { return this._companyId; } set { this._companyId = value; } }

        private System.String _weiXin;
        /// <summary>
        /// 微信号
        /// </summary>
        public System.String weiXin { get { return this._weiXin; } set { this._weiXin = value; } }

        private System.String _isValid;
        /// <summary>
        /// 状态
        /// </summary>
        public System.String isValid { get { return this._isValid; } set { this._isValid = value; } }

        private System.String _email;
        /// <summary>
        /// 私人邮箱
        /// </summary>
        public System.String email { get { return this._email; } set { this._email = value; } }

        private System.String _enterpriseQQ;
        /// <summary>
        /// 企业qq
        /// </summary>
        public System.String enterpriseQQ { get { return this._enterpriseQQ; } set { this._enterpriseQQ = value; } }

        private System.String _enterpriseEmail;
        /// <summary>
        /// 企业邮箱
        /// </summary>
        public System.String enterpriseEmail { get { return this._enterpriseEmail; } set { this._enterpriseEmail = value; } }

        private System.String _bank;
        /// <summary>
        /// 开户银行
        /// </summary>
        public System.String bank { get { return this._bank; } set { this._bank = value; } }

        private System.String _bankAccount;
        /// <summary>
        /// 银行账户
        /// </summary>
        public System.String bankAccount { get { return this._bankAccount; } set { this._bankAccount = value; } }

        private System.String _emergencyContact;
        /// <summary>
        /// 紧急联络人
        /// </summary>
        public System.String emergencyContact { get { return this._emergencyContact; } set { this._emergencyContact = value; } }

        private System.String _emergencyContactNumber;
        /// <summary>
        /// 紧急联络人电话
        /// </summary>
        public System.String emergencyContactNumber { get { return this._emergencyContactNumber; } set { this._emergencyContactNumber = value; } }

        private System.String _socialSecurityNumber;
        /// <summary>
        /// 社保个人编号
        /// </summary>
        public System.String socialSecurityNumber { get { return this._socialSecurityNumber; } set { this._socialSecurityNumber = value; } }

        private System.String _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String remark { get { return this._remark; } set { this._remark = value; } }

        private System.String _post;
        /// <summary>
        /// 岗位
        /// </summary>
        public System.String post { get { return this._post; } set { this._post = value; } }

        private System.String _avatar;
        /// <summary>
        /// 头像链接
        /// </summary>
        public System.String avatar { get { return this._avatar; } set { this._avatar = value; } }

        private System.Int32? _FixedBpoint;
        /// <summary>
        /// 固定积分
        /// </summary>
        public System.Int32? FixedBpoint { get { return this._FixedBpoint; } set { this._FixedBpoint = value; } }
    }
}