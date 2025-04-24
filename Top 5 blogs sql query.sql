SELECT TOP (5) [PostId]
      FROM [BloggingSite].[dbo].[PostComments] group by PostId order by count(Id) desc

  SELECT TOP (5) [PostId]
		FROM [BloggingSite].[dbo].[PostReactions] group by PostId order by count(Id) desc

  


												   select  [a].[Id] , [a].[Content]  from(
												                                    (select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs 
																								inner join  PostComments
																								on ApprovedBlogs.Id =PostComments.PostId) 
																		union all
																					(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs 
																						inner join  PostReactions
																						on ApprovedBlogs.Id =PostReactions.PostId)

																					) 
																					as a group by a.Id , a.Content  order by count(a.Id) desc;
													  
 



select * from ApprovedBlogs 
select * from PostComments;
select * from PostReactions;

delete from approvedBlogs where id = 24 

select approvedBlogs.Id, ApprovedBlogs.Content  From ApprovedBlogs 
 INNER JOIN PostComments 
	on ApprovedBlogs.Id = PostComments.PostId
INNER JOIN PostReactions
	on approvedBlogs.Id = PostReactions.PostId 
	group by approvedBlogs.Id, ApprovedBlogs.Content order by count(approvedBlogs.Id) desc
	OFFSET 0 ROWs 
	FETCH Next  5 rows only;