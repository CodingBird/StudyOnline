jQuery(function () {
    //获取移入目标
    var tar = jQuery('#nav ul.leftnav > li');
    //获取移入目标的高度 
    var tar_H = tar.outerHeight(true);
    //获取活动动作
    var who =
  tar.mouseover(function () {
      //活动移入目标索引;
      var i = who.index(this);
      //获取显示目标
      oTar = jQuery(this).children('.sub_nav');
      //获取显示目标实际高度     
      var oH = oTar.outerHeight(true);
      //获取当前li到顶部的高度
      var sH = tar_H * (i + 1);
      //判断显示目标位置
      var top = sH > oH ? -(oH - tar_H) : -(sH - tar_H + 1);
      //给移入目标加样式
      jQuery(this).addClass('on');
      //显示显示部分并设置位置
      oTar.show().css('top', top);
  }).mouseout(function () {
      jQuery(this).removeClass('on');
      oTar.hide();
  })
});