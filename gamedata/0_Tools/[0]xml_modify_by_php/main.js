(function ($, window, undefined) {
	$(function(){
		$('#file-o-dir').on('change', function(){
			var this_val = $(this).val();
			$('.file-o-list li:first').children('ul').remove();
			$.ajax({
				url: 'files-list.php',
				type: 'GET',
				data: 'file_o_dir='+this_val,
				async: false,
				success: function (data) {
					if (data != false) {
						$('.file-o-list li').append(data);
					}
				},
				cache: false,
				contentType: false,
				processData: false
			});
		});
		$('#file-r-dir').on('change', function(){
			var this_val = $(this).val();
			$('.file-r-list li:first').children('ul').remove();
			$.ajax({
				url: 'files-list.php',
				type: 'GET',
				data: 'file_r_dir='+this_val,
				async: false,
				success: function (data) {
					if (data != false) {
						$('.file-r-list li').append(data);
					}
				},
				cache: false,
				contentType: false,
				processData: false
			});
		});

		$('.yuding a').on('click', function(e){
			e.preventDefault();
			$(this).parent().addClass('active').siblings().removeClass('active');
			var id = $(this).data('id'),
					itemlist = $(this).data('itemlist'),					
					item = $(this).data('item'),
					tags = $(this).data('tags').split(','),
					child_itemlist = $(this).data('child-itemlist'),
					child_item = $(this).data('child-item'),
					child_tags = $(this).data('child-tags').split(','),
					dir = $(this).data('dir'),
					inputs = '';
			$('#guanlian').val(id);
			$('#itemlist').val(itemlist);
			$('#item').val(item);
			if (tags) {
				$.each( tags, function(i, n){
					inputs += '<p><input name="tags[]" type="text" value="' + n + '" required> *</p>';
				});
			}
			if (child_itemlist && child_item && child_tags) {
				inputs += '<h2 class="h2">特殊XML文件（替换数据在第3级Tag）</h2>';
				inputs += '<p><input name="child_itemlist" type="text" value="' + child_itemlist + '" required> *</p>';
				inputs += '<p><input name="child_item" type="text" value="' + child_item + '" required> *</p>';
				$.each( child_tags, function(i, n){
					inputs += '<p><input name="child_tags[]" type="text" value="' + n + '" required> *</p>';
				});
			}
			inputs += '<input name="dir" type="hidden" value="' + dir + '" required>';
			$('.tags').html(inputs);
		});
		$('#file-o').on('change', function(){
			var this_val = $(this).val();
			$('.yuding a').each(function(){
				if ( $(this).text() == this_val ) {
					$(this).click().parent().addClass('active').siblings().removeClass('active');
				}
			});
		});

		//目录方式切换
		$('#dir-mode-on').on('click', function(e){
			e.preventDefault();
			$('#file-o').attr('type','text').attr('name','filecn_dir');
			$('#file-r').attr('type','text').attr('name','fileen_dir');
			$(this).hide();
			$('#dir-mode-off').show();
			$('#file-o-dir,#file-r-dir').show();
		});
		$('#dir-mode-off').on('click', function(e){
			e.preventDefault();
			$('#file-o').attr('type','file').attr('name','filecn');
			$('#file-r').attr('type','file').attr('name','fileen');
			$(this).hide();
			$('#dir-mode-on').show();
			$('#file-o-dir,#file-r-dir').hide();
			$('.file-o-list li ul').remove();
			$('.file-r-list li ul').remove();
		});
		//目录方式原文件列表选择行为
		$('.file-o-list').on('click', 'a', function(e){
			e.preventDefault();
			$(this).addClass('haverun');
			var this_file_src = $(this).text(),
					this_file_name = $(this).data('file-name');
			$('#file-o').val(this_file_src);

			$('.yuding a').each(function(){
				if ( $(this).text() == this_file_name ) {
					$(this).click().parent().addClass('active').siblings().removeClass('active');
				}
			});
		});
		//目录方式被替换文件列表选择行为
		$('.file-r-list').on('click', 'a', function(e){
			e.preventDefault();
			$(this).addClass('haverun');
			var this_file_src = $(this).text(),
					this_file_name = $(this).data('file-name');
			$('#new_file_name').remove();
			$('#file-r').val(this_file_src).after('<input id="new_file_name" name="new_file_name" type="hidden" value="' + this_file_name + '" required>');
		});

		$('#add').on('click', function(e){
			e.preventDefault();
			$('.tags').append('<p><input name="tags[]" type="text" required> *</p>');
		});

		var process_num = 0;
		$('#form').submit(function(){
			$('body,html').animate({ scrollTop: 0 }, 600);
			var info_html = $('#runing .info').html();
			$('#runing .info').html('<p>' + (process_num++) + '. 原文件：'+$('#file-o').val()+'，替换文件：'+$('#file-r').val()+'</p>'+info_html);
			$('#runing .done, #runing .error').hide();
			$('#runing .loading').show();
			var formData = new FormData($(this)[0]);
			$('#runing').fadeIn(400, function(){
				$.ajax({
					url: 'run.php',
					type: 'POST',
					data: formData,
					async: false,
					success: function (data) {
						console.log(data);
						if (data == 'done') {
							$('#runing .loading').fadeOut(200);
							$('#runing .done').fadeIn(400);
						} else {
							$('#runing .loading').fadeOut(200);
							$('#runing .error').fadeIn(400);
						}
					},
					cache: false,
					contentType: false,
					processData: false
				});
			});

			return false;
		});

		$('#runing a').on('click', function(e){
			e.preventDefault();
			$('#runing').fadeOut(400);
		});

	});
})(jQuery, window);