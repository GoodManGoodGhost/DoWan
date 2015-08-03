using System;
using System.Collections.Generic;
using System.Text;

namespace Dowan
{
    /// <summary>
    /// 参数
    /// </summary>
    public class NetParams
    {


     //"pps":[{"ports":[8086],"ip":"119.97.172.89"},{"ports":[60],"ip":"183.61.2.159"},{"ports":[836],"ip":"119.84.76.201"}],

        public string opusDecoder;//":"http://c1.web.yy.com/r/rc/main/opusDecoder/1/1/opusDecoder.swf",
        public string amrEncoder;//":"http://c1.web.yy.com/r/rc/main/amrEncoder/1/4/amrEncoder.swf",
        public string defaultScene;//":"http://c1.web.yy.com/r/sc/main/defaultScene/1/82/defaultScene.swf",
        public string speexDecoder;//":"http://c1.web.yy.com/r/rc/main/speexDecoder/1/1/speexDecoder.swf",
        public string aacDecoder;//":"http://c1.web.yy.com/r/rc/main/aacDecoder/1/4/aacDecoder.swf",
        public string wyy;//":"3fb96f1310a24faeb751380b298e16a5",
        //public List<string> uploadLogoUrl;//":["http://uphdlogo.yy.com:8080/hdlogo"],
        public string token;//":"AAAADLwlZfDPmbv9Cgr-k9MQyCPMkPhSMlqjjDz7oSNR8Qv",
        public bool isAdWhite;//":true,
        public string aacEncoder;//":"http://c1.web.yy.com/r/rc/main/aacEncoder/1/6/aacEncoder.swf",
        public string hiido_ui;//":"0.021650000554339632",
        public bool isChannelWhite;//":true,
        //"verifyUrl":"/captcha/AAAADOwgwtewx3AhBi76ZuuCem49fAcDjdSig_Clp9LQe71A"
        public string verifyUrl;
        public bool openedVideo;//":true,
        public string amrDecoder;//":"http://c1.web.yy.com/r/rc/main/amrDecoder/1/4/amrDecoder.swf",
        public string loading;//":"http://c1.web.yy.com/r/rc/main/loading/1/7/loading.swf",
        public string publicModule;//":"http://c1.web.yy.com/r/rc/main/publicModule/1/28/publicModule.txt",
        public string silkDecoder;//":"http://c1.web.yy.com/r/rc/main/silkDecoder/1/5/silkDecoder.swf",
        public string publicModulev2;//":"http://c1.web.yy.com/r/rc/main/publicModulev2/1/56/publicModulev2.txt",
        public string opusEncoder;//":"http://c1.web.yy.com/r/rc/main/opusEncoder/1/1/opusEncoder.swf",
        public string eduModule;//":"http://c1.web.yy.com/r/rc/main/eduModule/1/20/eduModule.swf",
        //"pps":[{"ports":[75],"ip":"222.184.112.25"},{"ports":[836],"ip":"183.61.2.159"},{"ports":[695],"ip":"222.186.54.168"}],
        public List<PPS> pps;
        public string ip;//":"218.88.4.126",
        public bool allowAnonymous;//":true,
        public uint subSid;//":125233252,
        public uint topSid;//":9999,
        public string core;//":"http://c1.web.yy.com/r/rc/main/core/1/202/core.bmp"
        public uint auth;

    }
}
