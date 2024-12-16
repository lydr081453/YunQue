using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ESP.Supplier.Common
{
    public class VideoUtil
    {
        public static string PageCaptureHtml(string url, System.Text.Encoding encode)
        {
            WebRequest request = System.Net.HttpWebRequest.Create(url);
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                if (encode != null)
                {
                    using (StreamReader reader = new StreamReader(stream, encode))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }


        /**  

         * 获取视频信息  

         * @param url  

         * @return  

         */


        public static string getVideoInfo(String url)
        {
            string videoUrl = "";
            if (url.IndexOf("v.youku.com") != -1)
            {
                try
                {
                    videoUrl = getYouKuVideo(url);
                }
                catch (Exception e)
                {
                    videoUrl = string.Empty;
                }
            }
            else if (url.IndexOf("tudou.com") != -1)
            {
                try
                {
                    videoUrl = getTudouVideo(url);
                }
                catch (Exception e)
                {
                    videoUrl = string.Empty;
                }

            }
            else if (url.IndexOf("v.ku6.com") != -1)
            {
                try
                {
                    videoUrl = getKu6Video(url);
                }
                catch (Exception e)
                {
                    videoUrl = null;
                }
            }
            else if (url.IndexOf("6.cn") != -1)
            {
                try
                {
                    videoUrl = get6Video(url);
                }
                catch (Exception e)
                {
                    videoUrl = null;
                }

            }
            else if (url.IndexOf("56.com") != -1)
            {
                try
                {
                    videoUrl = get56Video(url);
                }
                catch (Exception e)
                {
                    videoUrl = null;
                }
            }
            return videoUrl;

        }

        /**  

         * 获取优酷视频  

         * @param url  视频URL  

         */

        public static string getYouKuVideo(String url)
        {

            string pageContent = PageCaptureHtml(url, Encoding.UTF8);
            string tmpContent = pageContent.Substring(pageContent.IndexOf("link2"), 200);
            string videoUrl = tmpContent.Substring(14, tmpContent.IndexOf("/>") - 16);
            return videoUrl;
        }





        //    /**  

        //     * 获取土豆视频  

        //     * @param url  视频URL  

        //     */ 
        public static string getTudouVideo(String url)
        {

            string pageContent = PageCaptureHtml(url, Encoding.UTF8);
            string tmpContent = pageContent.Substring(pageContent.IndexOf("defaultIid"), 80);
            string videoUrl = tmpContent.Substring(12);
            videoUrl = "http://www.tudou.com/v/" + videoUrl.Substring(0, videoUrl.IndexOf("\n")) + "/v.swf";
            return videoUrl;
        }

        //    public static Video getTudouVideo(String url) throws Exception{  

        //        Document doc = getURLContent(url);  

        //        String content = doc.html();  

        //        int beginLocal = content.IndexOf("<script>document.domain");  

        //        int endLocal = content.IndexOf("</script>");  

        //        content = content.Substring(beginLocal, endLocal);  



        //        /**  

        //         * 获取视频地址  

        //         */  

        //        String flash = getScriptVarByName("iid_code", content);  

        //        flash = "http://www.tudou.com/v/" + flash + "/v.swf";  



        //        /**  

        //         *获取视频缩略图   

        //         */ 

        //        String pic = getScriptVarByName("thumbnail", content);  



        //        /**  

        //         * 获取视频时间  

        //         */  

        //        String time = getScriptVarByName("time", content);  



        //        Video video = new Video();  

        //        video.setPic(pic);  

        //        video.setFlash(flash);  

        //        video.setTime(time);  



        //        return video;  

        //    }  





        //    /**  

        //     * 获取酷6视频  

        //     * @param url  视频URL  

        //     */ 
        public static string getKu6Video(String url)
        {
            string pageContent = PageCaptureHtml(url, Encoding.UTF8);
            string tmpContent = pageContent.Substring(pageContent.IndexOf("outSideSwfCode"), 200);
            tmpContent = tmpContent.Substring(tmpContent.IndexOf("value"));
            string videoUrl = tmpContent.Substring(7, tmpContent.IndexOf(".swf") - 3);
            
            return videoUrl;
        }
        //    public static Video getKu6Video(String url) throws Exception{  

        //        Document doc = getURLContent(url);  



        //        /**  

        //         * 获取视频地址  

        //         */ 

        //        Element flashEt = doc.getElementById("outSideSwfCode");  

        //        String flash = flashEt.attr("value");  



        //        /**  

        //         * 获取视频缩略图  

        //         */ 

        //        Element picEt = doc.getElementById("plVideosList");  

        //        String time = null;  

        //        String pic = null;  

        //        if(picEt!=null){  

        //            Elements pics = picEt.getElementsByTag("img");  

        //            pic = pics.get(0).attr("src");  



        //            /**  

        //             * 获取视频时长  

        //             */ 

        //            Element timeEt = picEt.select("span.review>cite").first();   

        //            time = timeEt.text();  

        //        }else{  

        //            pic = doc.getElementsByClass("s_pic").first().text();  

        //        }  



        //        Video video = new Video();  

        //        video.setPic(pic);  

        //        video.setFlash(flash);  

        //        video.setTime(time);  



        //        return video;  



        //    }  





        //    /**  

        //     * 获取6间房视频  

        //     * @param url  视频URL  

        //     */ 
        public static string get6Video(String url)
        {
            string pageContent = PageCaptureHtml(url, Encoding.UTF8);
            string tmpContent = pageContent.Substring(pageContent.IndexOf("video-share-code"));
            tmpContent = tmpContent.Substring(tmpContent.IndexOf("src"));
            string videoUrl = tmpContent.Substring(4, tmpContent.IndexOf(".swf")).Replace("&quot;","");

            return videoUrl;
        }

        //    public static Video get6Video(String url) throws Exception{  

        //        Document doc = getURLContent(url);  



        //        /**  

        //         * 获取视频缩略图  

        //         */ 

        //        Element picEt = doc.getElementsByClass("summary").first();  

        //        String pic = picEt.getElementsByTag("img").first().attr("src");  



        //        /**  

        //         * 获取视频时长  

        //         */ 

        //        String time = getVideoTime(doc, url, "watchUserVideo");  

        //        if(time==null){  

        //            time = getVideoTime(doc, url, "watchRelVideo");  

        //        }  



        //        /**  

        //         * 获取视频地址  

        //         */ 

        //        Element flashEt = doc.getElementById("video-share-code");  

        //        doc = Jsoup.parse(flashEt.attr("value"));    

        //        String flash = doc.select("embed").attr("src");  



        //        Video video = new Video();  

        //        video.setPic(pic);  

        //        video.setFlash(flash);  

        //        video.setTime(time);  



        //        return video;  

        //    }  





        //    /**  

        //     * 获取56视频  

        //     * @param url  视频URL  

        //     */ 
        public static string get56Video(String url)
        {
            //http://www.56.com/play_album-aid-9223215_vid-NjA3NTc3MTQ.html
            string temp = url.Substring(url.LastIndexOf("vid-")).Replace(".html", "");
            string videoUrl = "http://player.56.com/" + temp.Replace("vid-", "v_") + ".swf";
            //string pageContent = PageCaptureHtml(url, Encoding.UTF8);
            //string tmpContent = pageContent.Substring(pageContent.IndexOf("video-share-code"));
            //tmpContent = tmpContent.Substring(tmpContent.IndexOf("src"));
            //string videoUrl = tmpContent.Substring(4, tmpContent.IndexOf(".swf")).Replace("&quot;","");

            return videoUrl;
        }

        //    public static Video get56Video(String url) throws Exception{  

        //        Document doc = getURLContent(url);  

        //        String content = doc.html();  



        //        /**  

        //         * 获取视频缩略图  

        //         */ 

        //        int begin = content.IndexOf("\"img\":\"");  

        //        content = content.Substring(begin+7, begin+200);  

        //        int end = content.IndexOf("\"};");  

        //        String pic = content.Substring(0, end).trim();  

        //        pic = pic.replaceAll("\\\\", "");         



        //        /**  

        //         * 获取视频地址  

        //         */ 

        //        String flash = "http://player.56.com" + url.Substring(url.lastIndexOf("/"), url.lastIndexOf(".html")) + ".swf";  



        //        Video video = new Video();  

        //        video.setPic(pic);  

        //        video.setFlash(flash);  



        //        return video;  

        //    }  



        //    /**  

        //     * 获取56视频时长      

        //     */ 

        //    private static String getVideoTime(Document doc, String url, String id) {  

        //        String time = null;  



        //        Element timeEt = doc.getElementById(id);   

        //        Elements links = timeEt.select("dt > a");  





        //        for (Element link : links) {  

        //          String linkHref = link.attr("href");  

        //          if(linkHref.equalsIgnoreCase(url)){  

        //              time = link.parent().getElementsByTag("em").first().text();  

        //              break;  

        //          }  

        //        }  

        //        return time;  

        //    }  





        //    /**  

        //     * 获取script某个变量的值  

        //     * @param name  变量名称  

        //     * @return   返回获取的值   

        //     */ 

        //    private static String getScriptVarByName(String name, String content){  

        //        String script = content;  



        //        int begin = script.IndexOf(name);  



        //        script = script.Substring(begin+name.Length()+2);  



        //        int end = script.IndexOf(",");  



        //        script = script.Substring(0,end);  



        //        String result=script.Replace("'", "");  

        //        result = result.Trim();  



        //        return result;  

        //    }  





        //    /**  

        //     * 根据HTML的ID键及属于名，获取属于值  

        //     * @param id  HTML的ID键  

        //     * @param attrName  属于名  

        //     * @return  返回属性值  

        //     */ 

        //    private static String getElementAttrById(Document doc, String id, String attrName)throws Exception{  

        //        Element et = doc.getElementById(id);  

        //        String attrValue = et.attr(attrName);  



        //        return attrValue;  

        //    }  







        //    /**  

        //     * 获取网页的内容  

        //     */ 

        //    private static Document getURLContent(String url) throws Exception{  

        //        Document doc = Jsoup.connect(url)  

        //          .data("query", "Java")  

        //          .userAgent("Mozilla")  

        //          .cookie("auth", "token")  

        //          .timeout(6000)  

        //          .post();  

        //        return doc;  

        //    }  





        //    public static void main(String[] args) {  

        //        //String url = "http://v.youku.com/v_show/id_XMjU0MjI2NzY0.html";  

        //        //String url = "http://www.tudou.com/programs/view/pVploWOtCQM/";  

        //        //String url = "http://v.ku6.com/special/show_4024167/9t7p64bisV2A31Hz.html";  

        //        //String url = "http://v.ku6.com/show/BpP5LeyVwvikbT1F.html";  

        //        //String url = "http://6.cn/watch/14757577.html";  

        //        String url = "http://www.56.com/u64/v_NTkzMDEzMTc.html";  

        //        Video video = getVideoInfo(url);  

        //        System.out.println("视频缩略图："+video.getPic());  

        //        System.out.println("视频地址："+video.getFlash());  

        //        System.out.println("视频时长："+video.getTime());  

        //    }  

    }

}
