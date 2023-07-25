package SAR0083.Homework;

import java.time.Duration;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Arrays;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;
import org.springframework.web.servlet.HandlerInterceptor;
import org.springframework.web.servlet.ModelAndView;

@Component
public class RequestLoggingInterceptor implements HandlerInterceptor {
	// Properties
	private static final Logger logger = LoggerFactory.getLogger(RequestLoggingInterceptor.class);
	private static long requestId = 0;
	
	
	// Methods: override
	@Override
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
		Instant preHandleTime = Instant.now();
		
		request.setAttribute("requestId", requestId++);
		request.setAttribute("preHandleTime", preHandleTime);
		
		log(request, response, handler);
		
		return true;
	}

	@Override
	public void postHandle(HttpServletRequest request, HttpServletResponse response, Object handler, ModelAndView modelAndView) throws Exception {
		Instant postHandleTime = Instant.now();
		
		request.setAttribute("postHandleTime", postHandleTime);
		
		log(request, response, handler);
	}

	@Override
	public void afterCompletion(HttpServletRequest request, HttpServletResponse response, Object handler, Exception ex) throws Exception {
		Instant afterCompletionTime = Instant.now();
		
		request.setAttribute("afterCompletionTime", afterCompletionTime);
		
		log(request, response, handler);
	}
	
	private void log(HttpServletRequest request, HttpServletResponse response, Object handler) {
		// Build message
		String msg = "{} ID: {}, User: {} ({})\n"
		+ "- Request {"
			+ "Protocol: {}, "
			+ "Host: {}, "
			+ "URI: {}, "
			+ "Method: {}"
		+ "}\n"
		+ "- Response: {"
			+ "Status: {}"
		+ "}\n"
		+ "- Handler: {}\n"
		+ "- Pre handle time: {}\n";
		
		// Get all variables
		String tag = "[preHandle]";
		Instant preHandleTime = (Instant) request.getAttribute("preHandleTime");
		Instant postHandleTime = null;
		Instant afterCompletionTime = null;
		long totalHandleTime = 0;
		if(request.getAttribute("postHandleTime") != null) {
			msg += "- Post handle time: {}\n";
			tag = "[postHandle]";
			postHandleTime = (Instant) request.getAttribute("postHandleTime");
			totalHandleTime = Duration.between(preHandleTime, postHandleTime).toMillis();
		}
		if(request.getAttribute("afterCompletionTime") != null) {
			msg += "- After completion time: {}\n";
			tag = "[afterCompletion]";
			afterCompletionTime = (Instant) request.getAttribute("afterCompletionTime");
			totalHandleTime = Duration.between(preHandleTime, afterCompletionTime).toMillis();
		}
		msg += "- Total handle time: {} ms\n";
		
		// Get arguments
		ArrayList<Object> args = new ArrayList<Object>(Arrays.asList(new Object[] {
			tag,
			request.getAttribute("requestId"),
			request.getRemoteUser(),
			request.getRemoteAddr(),
			request.getProtocol(),
			request.getHeader("host"),
			request.getRequestURI(),
			request.getMethod(),
			response.getStatus(),
			handler,
			preHandleTime.toString()
		}));
		if(request.getAttribute("postHandleTime") != null) {
			args.add(postHandleTime.toString());
		}
		if(request.getAttribute("afterCompletionTime") != null) {
			args.add(afterCompletionTime.toString());
		}
		args.add(totalHandleTime);
		
		// Logging
		logger.info(msg, args.toArray());
	}
}