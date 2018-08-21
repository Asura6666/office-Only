/*
 Navicat Premium Data Transfer

 Source Server         : test001
 Source Server Type    : MySQL
 Source Server Version : 80011
 Source Host           : localhost:3306
 Source Schema         : mysqlssssssss

 Target Server Type    : MySQL
 Target Server Version : 80011
 File Encoding         : 65001

 Date: 21/08/2018 13:44:11
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for goods
-- ----------------------------
DROP TABLE IF EXISTS `goods`;
CREATE TABLE `goods`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `number` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `price` decimal(10, 2) NOT NULL,
  `description` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `state` varchar(5) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '待上架',
  `is_deleted` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '否',
  `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of goods
-- ----------------------------
INSERT INTO `goods` VALUES (1, '201519630000', '商品0000', 99.90, '描述0000', '已上架', '否', '2018-08-13 11:33:20', '2018-08-16 16:27:55');
INSERT INTO `goods` VALUES (2, '201519630001', '商品0001改了22', 999.99, 'string', '已上架', '否', '2018-08-13 11:33:47', '2018-08-16 16:41:46');
INSERT INTO `goods` VALUES (3, '201519630002', '曲奇饼干', 88.80, '描述啊444', '已上架', '否', '2018-08-13 16:55:44', '2018-08-16 16:27:56');
INSERT INTO `goods` VALUES (4, '201519630003', '旺仔小馒头', 7.90, '这是食物', '已上架', '否', '2018-08-13 17:01:10', '2018-08-16 16:27:56');
INSERT INTO `goods` VALUES (5, '201519630004', '又算食物又算服饰', 55.90, '这是食物和服饰的结合体', '已上架', '否', '2018-08-13 17:03:03', '2018-08-16 16:27:56');
INSERT INTO `goods` VALUES (6, '201519630005', '双标签3,4', 88.80, '双标签X2', '已下架', '否', '2018-08-13 17:06:05', '2018-08-14 15:38:22');
INSERT INTO `goods` VALUES (7, '201519630006', '华硕笔记本电脑', 4999.99, '这是电脑', '已下架', '否', '2018-08-13 17:12:15', '2018-08-14 15:38:22');
INSERT INTO `goods` VALUES (8, '201519630007', '测试用条目', 10.00, '测试用例', '待上架', '是', '2018-08-14 10:29:16', '2018-08-20 13:12:49');
INSERT INTO `goods` VALUES (11, '201519630008', '修改后的名字', 888.88, 'string改', '待上架', '是', '2018-08-14 14:14:38', '2018-08-20 13:13:02');
INSERT INTO `goods` VALUES (12, '201519630009', 'thisisanewtest', 99.98, '描述信息', '待上架', '否', '2018-08-16 14:26:20', '2018-08-20 13:13:07');
INSERT INTO `goods` VALUES (13, '201519630010', '商品0001改了22', 10.00, 'string', '待上架', '否', '2018-08-16 16:43:01', '2018-08-20 13:13:11');

-- ----------------------------
-- Table structure for goodtag
-- ----------------------------
DROP TABLE IF EXISTS `goodtag`;
CREATE TABLE `goodtag`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tag_id` bigint(20) NOT NULL,
  `good_id` bigint(20) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 25 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of goodtag
-- ----------------------------
INSERT INTO `goodtag` VALUES (1, 1, 1);
INSERT INTO `goodtag` VALUES (4, 4, 4);
INSERT INTO `goodtag` VALUES (5, 4, 3);
INSERT INTO `goodtag` VALUES (6, 4, 5);
INSERT INTO `goodtag` VALUES (7, 5, 5);
INSERT INTO `goodtag` VALUES (8, 3, 6);
INSERT INTO `goodtag` VALUES (9, 4, 6);
INSERT INTO `goodtag` VALUES (10, 5, 7);
INSERT INTO `goodtag` VALUES (11, 4, 8);
INSERT INTO `goodtag` VALUES (12, 5, 8);
INSERT INTO `goodtag` VALUES (13, 6, 8);
INSERT INTO `goodtag` VALUES (14, 1, 11);
INSERT INTO `goodtag` VALUES (15, 2, 11);
INSERT INTO `goodtag` VALUES (16, 3, 11);
INSERT INTO `goodtag` VALUES (17, 4, 11);
INSERT INTO `goodtag` VALUES (20, 3, 2);
INSERT INTO `goodtag` VALUES (21, 2, 2);
INSERT INTO `goodtag` VALUES (22, 4, 2);
INSERT INTO `goodtag` VALUES (23, 4, 13);
INSERT INTO `goodtag` VALUES (24, 5, 13);

-- ----------------------------
-- Table structure for tags
-- ----------------------------
DROP TABLE IF EXISTS `tags`;
CREATE TABLE `tags`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tag_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `is_deleted` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '否',
  `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tags
-- ----------------------------
INSERT INTO `tags` VALUES (1, '商品标签1', '否', '2018-08-13 11:28:31', '2018-08-13 11:29:25');
INSERT INTO `tags` VALUES (2, '标签2', '否', '2018-08-13 11:28:51', '2018-08-13 11:28:51');
INSERT INTO `tags` VALUES (3, '标签3', '否', '2018-08-13 11:28:58', '2018-08-13 11:28:58');
INSERT INTO `tags` VALUES (4, '食品', '否', '2018-08-13 15:26:08', '2018-08-13 15:26:31');
INSERT INTO `tags` VALUES (5, '服饰', '否', '2018-08-13 15:35:40', '2018-08-13 15:35:40');
INSERT INTO `tags` VALUES (6, '数码', '否', '2018-08-13 15:35:48', '2018-08-13 15:35:48');
INSERT INTO `tags` VALUES (7, '药物', '否', '2018-08-13 16:44:01', '2018-08-13 16:44:01');
INSERT INTO `tags` VALUES (8, '修改后的string', '是', '2018-08-13 18:29:07', '2018-08-13 18:49:56');
INSERT INTO `tags` VALUES (9, 'TestTag1', '否', '2018-08-14 10:05:30', '2018-08-14 10:05:30');
INSERT INTO `tags` VALUES (11, 'TestTag2', '是', '2018-08-14 13:51:22', '2018-08-14 13:52:47');

SET FOREIGN_KEY_CHECKS = 1;
