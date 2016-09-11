using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeChatApi.Entity.MsgEntity;
using System.Xml.Linq;

namespace WeChatApi.Service
{
    public class MsgFactory
    {
        private static List<BMsg> _queue;
        /// <summary>
        /// 加载消息，并绑定各消息类型的处理程序
        /// </summary>
        /// <param name="mheList">消息处理程序列表</param>
        public static void LoadMsg(EnterParam param, List<MsgHandlerEntity> mheList, Action<BaseMsg> baseCallBack = null)
        {
            //判断队列是否为空，为空则实例化。
            if (_queue == null)
            {
                _queue = new List<BMsg>();
            }
            else
            {
                //保留20秒内未响应的消息
                _queue = _queue.Where(q => q.CreateTime.AddSeconds(20) > DateTime.Now).ToList();
            }

            //获取数据包
            var postStr = Utils.GetRequestData(param);
            XElement xdoc = XElement.Parse(postStr);
            var msgtype = xdoc.Element("MsgType").Value.ToUpper();
            var FromUserName = xdoc.Element("FromUserName").Value;
            var CreateTime = xdoc.Element("CreateTime").Value;
            //获取消息类型
            MsgType type = (MsgType)Enum.Parse(typeof(MsgType), msgtype);
            //如果不是是事件类型
            if (type != MsgType.EVENT)
            {
                var MsgId = xdoc.Element("MsgId").Value;
                //判断队列中是否已经存在此消息，如果不存在，则将此消息加入队列中，否则返回null
                if (_queue.FirstOrDefault(m => m.MsgFlag == MsgId) == null)
                {
                    _queue.Add(new BMsg
                    {
                        CreateTime = DateTime.Now,
                        FromUser = FromUserName,
                        MsgFlag = MsgId
                    });
                }
                else
                {
                    return;
                }
            }
            else
            {
                //判断队列中是否已经存在此消息，如果不存在，则将此消息加入队列中，否则返回null
                if (_queue.FirstOrDefault(m => m.MsgFlag == CreateTime && m.FromUser == FromUserName) == null)
                {
                    _queue.Add(new BMsg
                    {
                        CreateTime = DateTime.Now,
                        FromUser = FromUserName,
                        MsgFlag = CreateTime
                    });
                }
                else
                {
                    return;
                }
            }



            //消息实体对象。
            BaseMsg msg = null;
            //事件类型。默认为NOEVENT，即非事件类型
            EventType eventtype = EventType.NOEVENT;
            switch (type)
            {
                //获取文本消息实体
                case MsgType.TEXT:
                    msg = Utils.ConvertObj<TextMsg>(postStr);
                    break;
                //获取图片消息实体
                //case MsgType.IMAGE: msg = Utils.ConvertObj<ImgMsg>(postStr); break;
                //获取视频消息实体
                //case MsgType.VIDEO: msg = Utils.ConvertObj<VideoMsg>(postStr); break;
                //获取音频消息实体
                //case MsgType.VOICE: msg = Utils.ConvertObj<VoiceMsg>(postStr); break;
                //获取链接消息实体
                //case MsgType.LINK: msg = Utils.ConvertObj<LinkMsg>(postStr); break;
                //获取地理位置消息实体
                case MsgType.LOCATION: msg = Utils.ConvertObj<LocationMsg>(postStr); break;
                //获取事件消息实体
                case MsgType.EVENT://事件类型
                    {
                        eventtype = (EventType)Enum.Parse(typeof(EventType), xdoc.Element("Event").Value.ToUpper());
                        switch (eventtype)
                        {
                            //取消订阅事件
                            case EventType.UNSUBSCRIBE:
                            //订阅事件
                            case EventType.SUBSCRIBE: msg = Utils.ConvertObj<SubEventMsg>(postStr); break;
                            //上报地理位置事件
                            case EventType.LOCATION: msg = Utils.ConvertObj<LocationEventMsg>(postStr); break;
                            //群发消息完成后，推送的数据包
                            case EventType.MASSSENDJOBFINISH:
                                msg = Utils.ConvertObj<GroupJobEventMsg>(postStr); break;
                        
                            case EventType.SCAN://扫描带参数二维码事件
                                msg = Utils.ConvertObj<ScanQrEventMsg>(postStr); break;
                            case EventType.CLICK:
                            
                            //自定义菜单扫描二维码
                            case EventType.SCANCODE_PUSH:
                            case EventType.SCANCODE_WAITMSG:
                                msg = Utils.ConvertObj<ScanMenuEventMsg>(postStr); break;

                            default:
                                msg = Utils.ConvertObj<EventMsg>(postStr); break;
                        }
                    } break;
                default:
                    msg = Utils.ConvertObj<BaseMsg>(postStr); break;

            }
            //执行基础回调
            if (baseCallBack != null)
            {
                baseCallBack(msg);
            }
            //根据根据消息类型，获取回调程序
            var ac = GetAction(mheList, type, eventtype);
            //如果回调程序存在，并且消息实体转换成功，则执行回调程序。
            if (ac != null && msg != null)
            {
                ac(msg);
            }
        }
        /// <summary>
        /// 根据消息类型，获取回调程序
        /// </summary>
        /// <param name="actions">回调委托列表</param>
        /// <param name="msgType">消息类型</param>
        /// <returns>回调委托</returns>
        private static Action<BaseMsg> GetAction(List<MsgHandlerEntity> mheList, MsgType msgType, EventType eventType)
        {
            MsgHandlerEntity temp = mheList.FirstOrDefault(mhe =>
            {
                if (msgType != MsgType.EVENT)
                    return mhe.MsgType == msgType;
                return mhe.EventType == eventType;
            });
            return temp.Action;
        }
    }
}
